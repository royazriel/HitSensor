//HitSensor.c


#include <pic.h>
#include "delay.h"
#include "usart.h"
#include "stdio.h"
#include "string.h"
#include "stdlib.h"

#define _XTAL_FREQ 8000000
#define IN_BUF_LEN 20
#define OUT_BUF_LEN 90
#define DEFAULT_THRESHOLD 50
#define DELAY_BETWEEN_SAMPLES_US 30

typedef enum __LogMode
{
	etFreeRunning = 1,
	etOnTreshhold	
}LogMode;

char buff[OUT_BUF_LEN];
char inBuf[IN_BUF_LEN];
char txBuf[IN_BUF_LEN];
char cnt = 0;
char msgReady;
short threshold = DEFAULT_THRESHOLD;
short delayBetweenSamples = DELAY_BETWEEN_SAMPLES_US;

LogMode mode = etOnTreshhold;



void interrupt isr(void)
{
	if (RCIF && RCIE )
	{
		if( cnt==IN_BUF_LEN ) cnt = 0;
		RCIF=0;
		inBuf[cnt] = RCREG;
		if(inBuf[cnt]=='\r') msgReady = 1;
		cnt++;
	}
}

void init()
{
	// Internal 4MHz RC
	OSCCON |= 0x72;					//!!!
	ANSEL=0;  //all digital
	ADON=0;
	TRISB1 = 0;
	RB1=0;
	
	TRISA0 	= 1;	//input
	ANS0 	= 1;	//RA0 is analog input
	ADCS0 	= 0;	//fastes FOSC/2
	ADCS1 	= 0;	//fastes FOSC/2
	CHS0  	= 0;	//RA0
	CHS1  	= 0;	//RA0
	CHS2  	= 0;	//RA0
	ADFM 	= 0;	//left justify
	ADON	= 1;	//turn on
	
	init_comms();
	__delay_ms(30);
	RCIE = 1;
	PEIE = 1;
	GIE = 1;
}

int main(void)
{
	int i,j;
	int buffEmpty = 1;
	init();
	while(1)
	{
		switch( mode )
		{
			case etFreeRunning:
				GODONE=1;	// initiate conversion on the selected channel
				while(GODONE);
				sprintf(txBuf,"%d\r\n",ADRESH);
				printf(txBuf);
				RB1^=1;			//measured ~=3.5ms per sample.
				break;
			
			case etOnTreshhold:
				if(buffEmpty)
				{
					GODONE=1;	// initiate conversion on the selected channel
					while(GODONE);
					if( ADRESH > threshold )
					{
						RB1=1;	
						for(i=0;i<OUT_BUF_LEN;i++)
						{
							GODONE=1;	// initiate conversion on the selected channel
							while(GODONE);
							buff[i] = ADRESH;
							for(j=0;j<delayBetweenSamples;j+=10)   //32us per sample step = 10 measured in this configuration ~= 10us delay
							{
								__delay_us(1);
							}
							RB1^=1;	
						}
						RB1=1;
						__delay_ms(90);
						__delay_ms(90);
						__delay_ms(90);
						RB1=0;
						buffEmpty=0;
					}
				}
				break;			
		}

		if( msgReady )
		{
			switch ( inBuf[0] )
			{
				case 't':
				 	threshold = atoi(inBuf+2);
					sprintf(txBuf,"new threshold: %d\r\n",threshold);
					printf(txBuf);
					break;
				case 'd':
					delayBetweenSamples = atoi(inBuf+2);
					sprintf(txBuf,"new delay: %d\r\n",delayBetweenSamples);
					printf(txBuf);
					break;
				case 'm':
				
					mode = atoi(inBuf+2);
					sprintf(txBuf,"new mode: %s\r\n",mode ==1 ? "etFreeRunning":"etOnTreshhold");
					printf(txBuf);
					memset(txBuf,0,IN_BUF_LEN);
					break;
				case 'r':
					if( !buffEmpty )
					{
						for(i=0;i<OUT_BUF_LEN;i++)
						{
							sprintf(txBuf,"%d\r\n",buff[i]);
							printf(txBuf);
						}
						putch('@');
					}
					else
					{
						sprintf(txBuf,"no results in buff\r\n");
						printf(txBuf);
					}
					buffEmpty = 1;	
			}
			msgReady = 0; cnt = 0;
		}
	}
}