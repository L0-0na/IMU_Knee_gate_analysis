/*
  Serial communicaton:
  (1) Send string from Arduino Pro-mini to PC (show on serial screen)
  (2) Receive string from PC (in serial screen), then show it again on PC
  
 Created 17 Dec. 2016
 This example code is in the public domain.

 http://engineer2you.blogspot.com
 */
String mySt;
char myChar = 0;
int i=0;

void setup() {
  // start serial port at 9600 bps:
  Serial.begin(9600);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
  }

  establishContact();  // send a byte to establish contact until receiver responds
}

void loop() {
  if (Serial.available() > 0) {
    myChar = Serial.read();
    mySt +=myChar;  //receive string from Computer
  }
  if ((mySt.length() >0)&(!Serial.available())) {
    Serial.print(mySt); //Print received string to Computer
    mySt="";
  }
}
void establishContact() {
  while (Serial.available() <= 0) {
    Serial.print("Arduino send: ");
    Serial.println(i);  //Print increasing value to Computer
    i+=1;
    delay(500);
  }
}
