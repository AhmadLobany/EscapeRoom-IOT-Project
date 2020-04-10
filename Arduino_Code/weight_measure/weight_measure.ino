

#include "HX711.h"
#include <LiquidCrystal_I2C.h>
#include<Wire.h>
#define DOUT  3
#define CLK  2



LiquidCrystal_I2C lcd(0x27,20,4);  // set the LCD address to 0x27 for a 16 chars and 2 line display

int wanted = 20;

HX711 scale;

float calibration_factor = -7050; //-7050 worked for my 440lb max scale setup

void setup() {
    lcd.init();                      // initialize the lcd 
  lcd.init();
  // Print a message to the LCD.
  lcd.backlight();
  
  scale.begin(DOUT, CLK);
  scale.set_scale();
  scale.tare(); //Reset the scale to 0
  
  long zero_factor = scale.read_average(); //Get a baseline reading
  lcd.clear();
  delay(500);
  lcd.print("Weight measure puzzle"); 
//  Serial.println(zero_factor);
delay(2000);
}

void loop() {

  scale.set_scale(calibration_factor); 
  lcd.clear();
  delay(500);
       lcd.setCursor(0,0);
  lcd.print("Reading:... ");
  delay(500);
  if((scale.get_units()-wanted)>5) {
      lcd.clear();
  delay(500);
       lcd.setCursor(0,0);
  lcd.print("Much");
           lcd.setCursor(0,1);
    lcd.print("Heavier!!");
  } else 
  {
    if((scale.get_units()-wanted)<-5){
            lcd.clear();
  delay(500);
       lcd.setCursor(0,0);
  lcd.print("Much");
           lcd.setCursor(0,1);
    lcd.print("Lighter!!");
    }else {
         lcd.clear();
       lcd.setCursor(0,0);
      lcd.print("Well Done");
       lcd.setCursor(0,1);
      lcd.print("Solved!!");
      delay(3000);    
        lcd.clear();
       lcd.setCursor(0,0);
      lcd.print("Save String3:");
       lcd.setCursor(0,1);
      lcd.print("53493utg");
      delay(60000);    
    }
  }
//  lcd.print(scale.get_units(), 1);
  delay(1000);
    char temp = scale.read();
    if(temp == '+' || temp == 'a')
      calibration_factor += 10;
    else if(temp == '-' || temp == 'z')
      calibration_factor -= 10;
}
