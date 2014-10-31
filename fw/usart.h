#ifndef _SERIAL_H_
#define _SERIAL_H_

#define BAUD 9600      
#define FOSC 8000000L
/* Use 9bit communication? FALSE=8bit */
#define NINE 0     

#define DIVIDER ((int)(FOSC/(16UL * BAUD) -1))
// Select 9600 on InternalRC 4MHz
//#define DIVIDER  25
#define HIGH_SPEED 1

#if NINE == 1
#define NINE_BITS 0x40
#else
#define NINE_BITS 0
#endif

#if HIGH_SPEED == 1
#define SPEED 0x4
#else
#define SPEED 0
#endif

#if defined(_16F87) || defined(_16F88)
	#define RX_PIN TRISB2
	#define TX_PIN TRISB5
#else
	#define RX_PIN TRISC7
	#define TX_PIN TRISC6
#endif

/* Serial initialization */
#define init_comms()\
	RX_PIN = 1;	\
	TX_PIN = 0;		  \
	SPBRG = DIVIDER;     	\
	RCSTA = (NINE_BITS|0x90);	\
	TXSTA = (SPEED|NINE_BITS|0x20)	
	



void putch(unsigned char);
unsigned char getch(void);
unsigned char getche(void);
#endif
