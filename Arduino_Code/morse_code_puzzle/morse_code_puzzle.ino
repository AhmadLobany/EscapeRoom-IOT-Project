
#include <Arduino.h>
#include <Wire.h>
#include <SoftwareSerial.h>
#include <LiquidCrystal_I2C.h>

LiquidCrystal_I2C lcd(0x27,20,4);  // set the LCD address to 0x27 for a 16 chars and 2 line display


int ledPin = 12;
int ledPin2 = 7;
int buttonPin=6;
const int TrigPin = 8;
const int EchoPin = 9;
unsigned long Time_Echo_us = 0; 
unsigned long Len_mm  = 0;  

int state=0;



int stateMorse=0;


unsigned int MSByteDist = 0;
unsigned int LSByteDist = 0;
unsigned int mmDist = 0;
int startM=0;

char* letters[] = {
".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", // A-I
".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", // J-R 
"...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." // S-Z
};

char* numbers[] = {
  "-----", ".----", "..---", "...--", "....-", ".....",
"-....", "--...", "---..", "----."
};


void setup() {
    Wire.begin();
    Serial.begin(9600);
      lcd.init();                      // initialize the lcd 
  lcd.init();
  lcd.backlight();
  pinMode(ledPin, OUTPUT);
    pinMode(ledPin2, OUTPUT);
        pinMode(buttonPin, INPUT);
    pinMode(EchoPin, INPUT);    //  The set EchoPin input mode.
 pinMode(TrigPin, OUTPUT);   //  The set TrigPin output mode.
}


  char* message = "S5S13S7S17S4";
  //S5S13S7S17S4
  static int Attempts=2;
  int c[5]={5,13,7,17,4};
  int counter=0;
  int start=0;
  int FirstInsert=0;



  
  void loop() {
   if(start==0) {
    lcd.clear();
    delay(500);
        lcd.setCursor(0,0);
      lcd.print("Click Button To");
              lcd.setCursor(0,1);
      lcd.print("Start .....");
     while(stateMorse!=HIGH) {
       stateMorse=digitalRead(buttonPin);
      }
      lcd.clear();
     }
  while(Attempts>0) {
    
    int i;
  for(i=0;i<3;i++){
      digitalWrite(ledPin2, HIGH);
     digitalWrite(ledPin, HIGH);
      delay(500);
      digitalWrite(ledPin2, LOW);
       digitalWrite(ledPin, LOW);
      delay(500);
  }
  int j=0;
 while (message[j]!='\0'){

      char ch = message[j];
  
if (ch >= 'a' && ch <= 'z')
{
flashSequence(letters[ch - 'a']);
}
else if (ch >= 'A' && ch <= 'Z') {
flashSequence(letters[ch - 'A']); }
else if (ch >= '0' && ch <= '9') {
flashSequence(numbers[ch - '0']); }
else if (ch == ' ') {
delay(800);
}
   j++;
   delay(2000);
  }
  Attempts--;
  if(Attempts==1) {
      lcd.clear();
    delay(500);
        lcd.setCursor(0,0);
      lcd.print("Broadcasting");
              lcd.setCursor(0,1);
      lcd.print("Again .....");
  }
  delay(3000);
    lcd.clear();
  start++;
  }   
     if(startM==0) {
      startM++;
    }
        lcd.setCursor(0,0);
      lcd.print("Click Button To");
              lcd.setCursor(0,1);
      lcd.print("Measure...");
       state=digitalRead(buttonPin);      
        if(state==HIGH) {
          delay(500);
   if(counter<5) {
       int i;
   for(i=0;i<3;i++){
      digitalWrite(ledPin2, HIGH);
     digitalWrite(ledPin, HIGH);
      delay(500);
      digitalWrite(ledPin2, LOW);
       digitalWrite(ledPin, LOW);
      delay(500);
  }
           digitalWrite(TrigPin, HIGH);                         // Send pulses begin by Trig / Pin
           delayMicroseconds(50);                               // Set the pulse width of 50us (> 10us)
           digitalWrite(TrigPin, LOW);                          // The end of the pulse    
           Time_Echo_us = pulseIn(EchoPin, HIGH);               // A pulse width calculating US-100 returned
           if((Time_Echo_us < 60000) && (Time_Echo_us > 1)) {   // Pulse effective range (1, 60000).
            Len_mm = (Time_Echo_us*34/100)/2;                   // Calculating the distance by a pulse width.   
            Len_mm=Len_mm-38;
            int dist=Len_mm/10;
                   lcd.clear();
                   delay(500);
//            if(FirstInsert!=0)  {
//            lcd.print("Distance is:");              // Output to the serial port monitor 
//            lcd.print(dist, DEC);                          // Output to the serial port monitor   
//            lcd.print("CM");                               // Output to the serial port monitor
//            delay(1500);
//            }
//            FirstInsert++;
            lcd.clear();
                        if( (dist-c[counter])<=1 && (dist-c[counter])>=-1) {
                    digitalWrite(ledPin2, HIGH);
                    delay(2000);
                    digitalWrite(ledPin2, LOW);
            }else {
                lcd.clear();
            delay(500);
        lcd.setCursor(0,0);
      lcd.print("Incorrect..");
              lcd.setCursor(0,1);
      lcd.print("Try Again");
               startM=0;
               counter=-1;
                    digitalWrite(ledPin, HIGH);
                    delay(2000);
                    digitalWrite(ledPin, LOW);
                    
            }
           }  
           delay(2000);                                         
           counter++;
    }
    if(counter>=5) {
      lcd.clear();
    delay(500);
       lcd.setCursor(0,0);
      lcd.print("Congrats!");
       lcd.setCursor(0,1);
      lcd.print("Solved ...");
      delay(3000);
            lcd.clear();
       lcd.setCursor(0,0);
      lcd.print("Save String1:");
       lcd.setCursor(0,1);
      lcd.print("jh78t3h9");
      delay(60000);
        counter=0;
         start=0;
        FirstInsert=0;
        Attempts=2;
       startM=0;
       state=0;
      stateMorse=0;
    }
    }

}

void flashSequence(char* sequence) {
int i = 0;
while (sequence[i]!='\0') {
        flashDotOrDash(sequence[i]);
i++; }
delay(2000);
}


void flashDotOrDash(char dotOrDash) {
 if (dotOrDash == '.')
{
  digitalWrite(ledPin, HIGH);
    delay(500);
    digitalWrite(ledPin, LOW);
    delay(500);
  }
else //  a - 
{
    digitalWrite(ledPin2, HIGH);
    delay(1500); 
    digitalWrite(ledPin2, LOW);
    delay(500);
}
}
