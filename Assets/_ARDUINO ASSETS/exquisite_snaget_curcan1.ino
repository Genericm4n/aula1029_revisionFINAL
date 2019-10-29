// Digital
int btnLeft = 12;
int btnDown = 11;
int btnRight = 10;
int btnUp = 9;

// Start and Select
int btnSelect = 8;
int btnStart = 7;

// Action Buttons
int btnB = 6;
int btnA = 5;
int btnY = 4;
int btnX = 3;

// Light Sensor
int sensorLight = A0;

void setup()
{
  pinMode(btnLeft,INPUT);
  pinMode(btnDown,INPUT);
  pinMode(btnRight,INPUT);
  pinMode(btnUp,INPUT);
  
  pinMode(btnSelect, INPUT);
  pinMode(btnStart, INPUT);
  
  pinMode(btnB, INPUT);
  pinMode(btnA, INPUT);
  pinMode(btnY, INPUT);
  pinMode(btnX, INPUT);
  
  pinMode(sensorLight, INPUT);
  
  Serial.begin(9600);
}

void loop()
{
  int readBtnLeft = digitalRead(btnLeft);
  int readBtnDown = digitalRead(btnDown);
  int readBtnRight = digitalRead(btnRight);
  int readBtnUp = digitalRead(btnUp);
  
  int readBtnSelect = digitalRead(btnSelect);
  int readBtnStart = digitalRead(btnStart);
  
  int readBtnB = digitalRead(btnB);
  int readBtnA = digitalRead(btnA);
  int readBtnY = digitalRead(btnY);
  int readBtnX = digitalRead(btnX);
  
  int readSensor = analogRead(sensorLight);
  
  Serial.print(readBtnLeft);
  Serial.print(";");
  Serial.print(readBtnDown);
  Serial.print(";");
  Serial.print(readBtnRight);
  Serial.print(";");
  Serial.print(readBtnUp);
  Serial.print(";");
  Serial.print(readBtnSelect);
  Serial.print(";");
  Serial.print(readBtnStart);
  Serial.print(";");
  Serial.print(readBtnB);
  Serial.print(";");
  Serial.print(readBtnA);
  Serial.print(";");
  Serial.print(readBtnY);
  Serial.print(";");
  Serial.print(readBtnX);
  Serial.print(";");
  Serial.println(readSensor);
  
  delay(10);
}