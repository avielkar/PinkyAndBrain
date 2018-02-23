int clkPin = 2;
int dataPin = 3;
int count = 300;
uint8_t color = 255;
bool init_stage = true;
bool color_stage = false;
bool places_stage = false;
bool command_stage = false;

String currentColor;

byte colors[4];
byte colorIndex;

#define NUM_OF_LEDS 150

#define NUM_OF_FRAMES 10

byte placesData[NUM_OF_LEDS * NUM_OF_FRAMES];
int numOfPlacedData;



void setup() {
  // put your setup code here, to run once:
  Serial.begin(2000000);
  //pinMode(clkPin , OUTPUT);
 // pinMode(dataPin , OUTPUT);
  //placesData = new byte[250];
  colorIndex = 0;
}

void loop() {
  // put your main code here, to run repeatedly:
  if(Serial.available() > 0)
  {
    if(init_stage)
    {
      char input = Serial.read();
      if(input == '#')
      {
        //Serial.print('#');
        init_stage = false;
        color_stage = true;
      }
    }
    else if (color_stage)
    {
      //not yet get all the 3 colors with brightness at first place.
      if(colorIndex < 4)
      {
        if(Serial.available())
        {
          colors[colorIndex] = Serial.read();
          colorIndex++;
        }
      }
      else if(Serial.available() > 0)
      {
        if(Serial.read() == '@')
        {
          //Serial.print('@');
          color_stage = false;
          places_stage = true;
          colorIndex = 0;
        }
      }
    }
    else if(places_stage)
    {
      byte data = 0;
      if(Serial.available() > 0)
      {
        data = Serial.read();
        //Serial.print('d');
        placesData[numOfPlacedData] = data;
        numOfPlacedData++;
      }
      if(numOfPlacedData == NUM_OF_LEDS * NUM_OF_FRAMES)
      {
        places_stage = false;
        command_stage = true;
      }
    }
    else  //command_stage.
    {
      if(Serial.available() > 0)
      {
        if(Serial.read() == '!')
        {
          //Serial.print("arduino execution begin\n");
          for(int i = 0; i < NUM_OF_FRAMES; i++)
          {
            LedStripRoundDataExecution();
            delay(90);
          }
          command_stage = false;
          init_stage = true;
          numOfPlacedData = 0;
          //Serial.print("arduino execution over\n");
        }
      }
    }
  }
}

void PrintData()
{
}

void LedStripRoundDataExecution()
{
  startFrame();
  byte currentIndex = placesData[0];
  byte placeIndex = 1;
  for(byte i=0;i<(byte)NUM_OF_LEDS;i = i +(byte)(1))
  {
    if(placesData[i] == 1)
    {
      sendColor(colors[1] , colors[2] , colors[3] , colors[0]);
    }
    else
    {sendColor(0 , 0 , 0, 0);}
  }
  
  endFrame(250);
}

void transferByte(uint8_t data)
{
      digitalWrite(dataPin, data>> 7 & 1);
      digitalWrite(clkPin, HIGH);
      digitalWrite(clkPin, LOW);
      digitalWrite(dataPin, data>> 6 & 1);
      digitalWrite(clkPin, HIGH);
      digitalWrite(clkPin, LOW);
      digitalWrite(dataPin, data>> 5 & 1);
      digitalWrite(clkPin, HIGH);
      digitalWrite(clkPin, LOW);
      digitalWrite(dataPin, data>> 4 & 1);
      digitalWrite(clkPin, HIGH);
      digitalWrite(clkPin, LOW);
      digitalWrite(dataPin, data>> 3 & 1);
      digitalWrite(clkPin, HIGH);
      digitalWrite(clkPin, LOW);
      digitalWrite(dataPin, data>> 2 & 1);
      digitalWrite(clkPin, HIGH);
      digitalWrite(clkPin, LOW);
      digitalWrite(dataPin, data>> 1 & 1);
      digitalWrite(clkPin, HIGH);
      digitalWrite(clkPin, LOW);
      digitalWrite(dataPin, data>> 0 & 1);
      digitalWrite(clkPin, HIGH);
      digitalWrite(clkPin, LOW);
}

void sendColor(uint8_t red, uint8_t green, uint8_t blue, uint8_t brightness)
{
      transferByte(0b11100000 | brightness);
      transferByte(blue);
      transferByte(green);
      transferByte(red);
}

void startFrame()
{
      initiialize();
      transferByte(0);
      transferByte(0);
      transferByte(0);
      transferByte(0);
}

void initiialize()
{
      digitalWrite(dataPin, LOW);
      pinMode(dataPin, OUTPUT);
      digitalWrite(clkPin, LOW);
      pinMode(clkPin, OUTPUT);
}

void endFrame(uint16_t count)
{
      /* The data stream seen by the last LED in the chain will be delayed by
       * (count - 1) clock edges, because each LED before it inverts the clock
       * line and delays the data by one clock edge.  Therefore, to make sure
       * the last LED actually receives the data we wrote, the number of extra
       * edges we send at the end of the frame must be at least (count - 1).
       *
       * Assuming we only want to send these edges in groups of size K, the
       * C/C++ expression for the minimum number of groups to send is:
       *
       *   ((count - 1) + (K - 1)) / K
       *
       * The C/C++ expression above is just (count - 1) divided by K,
       * rounded up to the nearest whole number if there is a remainder.
       *
       * We set K to 16 and use the formula above as the number of frame-end
       * bytes to transferByte.  Each byte has 16 clock edges.
       *
       * We are ignoring the specification for the end frame in the APA102
       * datasheet, which says to send 0xFF four times, because it does not work
       * when you have 66 LEDs or more, and also it results in unwanted white
       * pixels if you try to update fewer LEDs than are on your LED strip. */

      for (uint16_t i = 0; i < (count + 14)/16; i++)
      {
        transferByte(0);
      }

      /* We call initiialize() here to make sure we leave the data line driving low
       * even if count is 0. */
      initiialize();
}

