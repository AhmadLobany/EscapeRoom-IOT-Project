#include <Wire.h>
#include <LiquidCrystal_I2C.h>

#define S0 10
#define S1 11
#define S2 6
#define S3 3
#define sensorOut 4
#define BLUE 1
#define RED 2
#define GREEN 3
const int blue =13 ;
const int red = 8;
const int green = 12;

LiquidCrystal_I2C lcd(0x27,20,4);  // set the LCD address to 0x27 for a 16 chars and 2 line display


int touch = 5; // Use Pin 10 as our Input



int frequency = 0;
int Blue,Green,Red;
void setup() {
  Wire.begin();
    Serial.begin(9600);
      lcd.init();                      // initialize the lcd 
  lcd.init();
  lcd.backlight();
  pinMode(S0, OUTPUT);
  pinMode(S1, OUTPUT);
  pinMode(S2, OUTPUT);
  pinMode(S3, OUTPUT);
  pinMode(sensorOut, INPUT);
  pinMode (touch, INPUT);
  
  // Setting frequency-scaling to 20%
  digitalWrite(S0,HIGH);
  digitalWrite(S1,LOW);
  pinMode(blue, OUTPUT);//set blue as an output
  pinMode(red, OUTPUT);//set red as an output
  pinMode(green, OUTPUT);//set green as an output
}
   int arr[12] = {RED,RED,RED,BLUE,RED,BLUE,RED,BLUE,GREEN,RED,BLUE,GREEN};
   int counter=0;

   
void loop() {
  lcd.clear();
  digitalWrite(S2,LOW);
  digitalWrite(S3,LOW);
  // Reading the output frequency
  frequency = pulseIn(sensorOut, LOW);
  //Remaping the value of the frequency to the RGB Model of 0 to 255
  Red = frequency = map(frequency, 25,72,255,0);
  // Printing the value on the serial monitor
  Serial.print("R= ");//printing name
  Serial.print(frequency);//printing RED color frequency
  Serial.print("  ");
  delay(100);
  // Setting Green filtered photodiodes to be read
  digitalWrite(S2,HIGH);
  digitalWrite(S3,HIGH);
  // Reading the output frequency
  frequency = pulseIn(sensorOut, LOW);
  //Remaping the value of the frequency to the RGB Model of 0 to 255
  Green = frequency = map(frequency, 30,90,255,0);
  // Printing the value on the serial monitor
  Serial.print("G= ");//printing name
  Serial.print(frequency);//printing RED color frequency
  Serial.print("  ");
  delay(100);
  // Setting Blue filtered photodiodes to be read
  digitalWrite(S2,LOW);
  digitalWrite(S3,HIGH);
  // Reading the output frequency
  frequency = pulseIn(sensorOut, LOW);
  //Remaping the value of the frequency to the RGB Model of 0 to 255
  Blue= frequency = map(frequency, 25,70,255,0);
  // Printing the value on the serial monitor
  Serial.print("B= ");//printing name
  Serial.print(frequency);//printing RED color frequency
  Serial.println("  ");
  delay(100);
  if(Green<50 && Blue<50 && Red<200
  ) {
       // digitalWrite(green, HIGH);
               digitalWrite(red, HIGH);
        digitalWrite(blue, HIGH);

        delay(1000);
        //digitalWrite(green, LOW);
                digitalWrite(red, LOW);
        digitalWrite(blue, LOW);

  } else {
  if(Green>Blue)
  {
    if(Green>Red) {
      lcd.print("Color: Green\n");
       digitalWrite(green, HIGH);
        delay(1000);
        digitalWrite(green, LOW);
        if(arr[counter]==GREEN) counter++;
      }
    else{
       lcd.print("Color: Red\n");
        digitalWrite(red,HIGH);

       delay(1000);
       digitalWrite(red, LOW);


        if(arr[counter]==RED) counter++;

    }
  } else {
    if(Blue>Red) {
           lcd.print("Color: Blue\n");
                    digitalWrite(blue,HIGH);
       delay(1000);
       digitalWrite(blue, LOW);
               if(arr[counter]==BLUE) counter++;
    } else {
            lcd.print("Color: Red\n");
                    digitalWrite(red,HIGH);


       delay(1000);
       digitalWrite(red, LOW);

               if(arr[counter]==RED) counter++;
    }
  }
  if(counter==6) {
    lcd.clear();
                lcd.setCursor(0,0);
      lcd.print("Well done!");
              lcd.setCursor(0,1);
      lcd.print("Guessed 6...!");
  }}
  if(counter>=12) {
    lcd.clear();
    delay(500);
            lcd.setCursor(0,0);
      lcd.print("Well done!");
              lcd.setCursor(0,1);
      lcd.print("Puzzle Solved!");
      delay(5000);
      lcd.clear();
      delay(500);
             lcd.setCursor(0,0);
      lcd.print("Save String2:");
       lcd.setCursor(0,1);
      lcd.print("457h5y5i");
      delay(60000);
      counter=0;
  }
  delay(1000);
}
