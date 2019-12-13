# Library to Evaluate a Bowling Game and return the total Score

# Expected input:
String - Bowling score table in the format of: X|7/|9-|X|-8|8/|-6|X|X|X||81
where each frame is separated by |, || indicates the end of the game followed by any bonus throws earned in the final frame
digits in between indicate the result of each throw:
* 1 through 9 indicating the number of pins knocked down on a throw,
* "-" is no pins, 
* X is a strike (all 10 pins on the first throw), 
* / is a spare (knocking down all remaining pins left after the first throw).

Five Pin includes other allowed values:
* H HeadPin - only got the 5 pin
* L or R CornerPin - only one of the 2 pins is left
* A Ace - left both 2 pins
* C Chop - Headpin and one full side (both the 2 and 3 pins)
* S Split - HeadPin and one of the 3 pins
Five Pin also has up to 3 rolls per frame with no bonus throws as the last frame should always have 3 rolls
ex: X|7/|A--|X|-8-|8/|--6|X|-/|5/5||

Enum - Game Type indicating the format to expect: TenPin or FivePin (defaults to TenPin)

# Expected Output:
Integer - total score of the entire Game