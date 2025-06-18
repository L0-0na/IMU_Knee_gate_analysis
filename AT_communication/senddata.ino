/* ***********************************************************
 *                          Library                          *
 * ********************************************************* */
#include <Wire.h>


/* ***********************************************************
 *                      Global Constants                     *
 * ********************************************************* */
const int MPU_addr_1 = 0x68;                                                    // I2C address of MPU-6050
const int MPU_addr_2 = 0x69;

int iAcX1 = 3, iAcY1 = 4, iAcZ1 = 5, iAcX2 = 6, iAcY2 = 7, iAcZ2 = 8;           // Acceleration index for the two MPU
int iGyX1 = 9, iGyY1 = 10, iGyZ1 = 11, iGyX2 = 12, iGyY2 = 13, iGyZ2 = 14;      // Gyro index for the two MPU


/* ***********************************************************
 *                      Global Variables                     *
 * ********************************************************* */
//accelerometer - force it to be a 16-bit integer
//gyroscope - force it to be a 16-bit integer

byte MPU_FRAME[16];                    //MPU Frame capsulate the two MPU data sample
boolean f_3ms = 0;                        //flage raise when the time interrupt happen
boolean toggle0 = 0;
unsigned long inst_time;

uint32_t timer;
/* ***********************************************************
 *                         Void Setup                        *
 * ********************************************************* */
void setup() {

  Serial2.begin(115200);
  Serial.begin(115200);
  
    cli();//stop interrupts
  // set timer1 interrupt at 300Hz
  TCCR1A = 0;// set entire TCCR1A register to 0
  TCCR1B = 0;// same for TCCR1B
  TCNT1  = 0;// initialize counter value to 0
  // set compare match register for 300hz increments
  OCR1A = 2499;// = (16*10^6) / (300*64) - 1 (must be <65536)
  // turn on CTC mode
  TCCR1B |= (1 << WGM12);
  // Set CS10 and CS12 bits for 64 prescaler
  TCCR1B |= (1 << CS11) | (1 << CS10);  
  // enable timer compare interrupt
  TIMSK1 |= (1 << OCIE1A);
  sei();//allow interrupts
}


/* ***********************
 *  Timer Interruption   *
 *********************** */
ISR(TIMER1_COMPA_vect){//timer1 interrupt 300 Hz to take a sample
  //Serial.print("flag  ");
  f_3ms = 1;
}


/* ***********************************************************
 *                         Void Loop                         *
 * ********************************************************* */
void loop() {
  double dt = (double)(micros() - timer) / 1000000; // Calculate delta time
  timer = micros();
  if(f_3ms == 1){
     MPU_FRAME[0] = 0xFF; //header1
     MPU_FRAME[1] = 0xFF; //header2
     MPU_FRAME[2] = 0xFF; //header3
     
     for(int i=3; i<15; i++)
        MPU_FRAME[i] = dt + i;
    
  MPU_FRAME[15] = 0;
    for( int i = 0; i<15; i++){
      MPU_FRAME[15] = MPU_FRAME[15] ^ MPU_FRAME[i]; //ETX
    //Serial2.print(" ");
  }
    //Print the output to the serial monitor
  for( int i = 0; i<15; i++){
    Serial2.write(MPU_FRAME[i]);
  }
  //Serial2.println();
  Serial.println(dt);
    f_3ms = 0;
  }

}
