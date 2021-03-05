# Pico y placa predictor
Project to determine if a car with a license plate number can circulate at a certain date and time.

#### You can [enter here](https://picoyplaca.azurewebsites.net/) to see the last deploy on Azure

## Starting
In Ecuador license plates for cars follow this format [AAA-0000]
3 letters followed by 3 or 4 numbers, the first letter identifies a province of the following list (letters D and F are not used):

A Azuay -
B Bolivar -
C Carchi -
E Esmeraldas -
G Guayas -
H Chimborazo -
I Imbabura -
J Santo Domingo -
K Sucumbios -
L Loja  -
M Manabi -
N Napo -
O El oro -
P Pichinca -
Q Orellana -
R Los rios -
S Pastaza -
T Tungurahua -
U Cañar -
V Morona Santiago -
W Galapagos -
X Cotopaxi -
Y Santa Elena -
Z Zamora Chinchipe

Pico y placa is system used in the city of Quito-Ecuador to prevent vehicular traffic, from 7:00 to 9:30 and 16:00 to 19:30

| Restriction day   | License plate last digit |
|-------------------|--------------------------|
| Monday            | 1,2                      |
| Tuesday           | 3,4                      |
| Wednesday         | 5,6                      |
| Thursday          | 7,8                      |
| Friday            | 9,0                      |
| Saturday & sunday | Free mobility            |




## Input format
#### License plate number (string) => xxx-0000
Admits 3 letters Uppercase followed by a hyphen and at least 3 numbers


#### Date (string) 
Admits an string with the format dd/mm/yyyy (days between 1 and 31, month between 1-12 and year until 3000)

#### Time (string)
Admits hh:mm format (hours between 00 and 23 and minutes between 00 and 59)

## Response messages
Each field requires the correct input of string, this format is compared using regular expresion, and different types of warnings about format are diplayed on the screen.
#### Warning for invalid dates i.e. (31/02/2020) 
El día ingresado es inexistente, prueba otra fecha
#### Warning for invalid license plates i.e. (asd-4510) 
Formato de placa: XXX-0000
#### Warning for date fortmat i.e (14/15/1997)
Formato de fecha: dd/mm/yyyy
#### Warning for hour format i.e (24:66)
Formato de hora: hh:mm
#### Success messages after verification (can´t ride today)
SU VEHÍCULO NO PUEDE CIRCULAR ESTE DÍA (day and date) (07:00-9:30am / 16:00-19:30) 
#### Success messages after verification (can ride today)
TIENE LIBRE MOVILIDAD PARA ESTA FECHA (day and date) Y HORA (hour)


## Screenshots
### Desktop view
![img1](https://user-images.githubusercontent.com/45187015/110157957-c8b8b480-7db6-11eb-903d-cfe87c622a57.png)

### Smartphone view
![img2](https://user-images.githubusercontent.com/45187015/110157780-973fe900-7db6-11eb-9a54-ec161fa5bfca.jpeg)
