# Vampizza
Vampizza is a Isometric Tycoon Farming game based around a vampire traveling through time to deliver and fufill the most delicous pizza. 
Players play as the vampire establishing and building up an empire that specializes in pizza making all while maintaining anonymity and their thirst for blood.

		
## About
#### Gameplay Genre
This is a **2D Isometric Tycoon Resource-Based Farming Simulator**

*The genre may be updated in future release to add turn based fighting mechanics.*

### Game Engine 
Unity 2020.1.0
### Current Version of Vampizza 
*-0.0.1*

## Get Involved
If you're a student looking to help make a game whether you're an art student, C.S. student, music student, or similar you're welcome to join! We also appreciate those who want to help test the game, or provide mentorship in our quest to develop in Unity. First head on over to the [project discord](https://discord.gg/vhcHKKP9df) to communicate with others who are there. There might not be many of us there, but you're welcome to join us. We follow "due dates" to ensure things get done at some point. Eventually, we will also limit the number of developers. 

## How to Play
		
# Player
### Level
Level of the player represents the "strength" of the player. 
Levels are gained through experience rewards. There is no **maximum** level allowing 
the game to be ever scaling. A level can directly effect the difficulty of the game 
as various mechanics may rely on it for their difficulty.
				
Experience can be gained through:
- Unlocking/Upgrading/Building Town Structures
- Gathering Resources
- Completing Missions/Orders

### Blood Thirst Meter
The thirst meter represents the player's need to feed on human blood. 
A full meter is a full vampire while an empty meter is a **very thirsty** 
vampire. The consequence of an empty meter is the loss of citizen count 
equal to the vampire's level (**One towny loss per every player level**). 
The player will also experience the loss of 0.10 times the number of levels
in currency to pay for the loss of citizens. The amount of blood needed 
**daily** also depends on the player's level where 10 units
of blood is needed per every level. A level 20 vampire will need 200 units 
of blood to be sustained and will lose 20 townies upon an empty meter

To learn more about the resource: Blood scroll down to resources.
				
*Fun Fact: The average human is around 10 units of blood (1.2-1.5 gallons) - American Red Cross*

### Inventory
The location for all of the player's resources excluding Blood and currency. 
Maximum allowed space depends entirely on the built structures in the player's
town. To increase the maximum allowed items in inventory, upgrading and purchasing 
Storage Facilities is required. The player initially starts at a maximum of **500**
items allowed in inventory. 
				
### Currency
This is the driving force in upgrading the player's town. Currency allows the 
player to purchase structures, resources, items, and upgrades. The form of currency evolves 
alongside the town and it's technology. 
- Medieval Era: Coins
- Other Eras TBA
			
 ### Pizza
The player's main focus should be to make pizza to attract tourists to their town.
Pizzas can be made through resources gathered by town efforts. Pizza isn't an actual inventory
item but instead recipe lists and how many pizzas can be made with each. 
		
# Town 
## Attributes
### Citizen Count
There are a number of citizens that belong to the Vampire Player's town. Building 
towny homes allows for citizens to move in to the town and contribute to 
the pizza making that the town is famous for. Each citizen in the player's town represents 
0.025x production speed on farm plots, and 
crafting stations
				
				
### Technological Era
Vampires are immortal and thus live until they are are murdered. 
To incorporate this aspect in to the game, the player can experience different 
timelines where their upgrades, currency, structures, etc change with the current era. Some 
structures, resources, and upgrades are only unlocked through entering a new era. 

## Resources
### Blood
There is a meter that the user must always be conscious of. The meter is measured in "units" of blood and must remain filled otherwise penalties occur. 
These penalties include:
- Loss of 1 citizen/worker per every level. If the player is a level 5, they will lose 5 citizens. 
- Loss of 10 times the number of levels in currency to pay for the loss of citizens. 
### Wheat
Wheat is a farmable resource. It can be used to create a vital resource such as flour to make dough for pizza orders. 
Wheat can ONLY be gathered through farm plots.

|Name in Game   | Type of Resource  | Used for | Base Selling Price   |
|---|---|---|---|
| Wheat  | Primitive Farming  | Crafting Flour  | 5 |


### Tomato
Tomato is a farmable resource. It is a resource needed for fulfilling recipes asked for through customer/tourist scenario interactions.
Tomato can only be gathered through farm plots.  

|Name in Game   | Type of Resource  | Used for | Base Selling Price   |
|---|---|---|---|
| Tomato | Primitive Farming  | Order Fulfilment/Delivery | 7 |

*Tomatos are a primitive resource that does not need refining*

### Milk
Milk is a primitive resource that can be gathered through dairy farms/barns. Barns are an unlockable structure 
needed to gather milk. Milk also **must** be crafted into cheese order to be used. 

|Name in Game   | Type of Resource  | Used for | Base Selling Price   |
|---|---|---|---|
| Milk  | Primitive  | Crafting Cheese  | 10 |


### Flour
Flour is a crafted resource needed for fulfilling recipes asked for through customer/tourist scenario interactions.
Flour can be crafted using wheat that is a gathered primitive resource. 

|Name in Game   | Type of Resource  |  Ingredients needed |  Used for | Base Selling Price   |
|---|---|---|---|---|
| Flower  | Crafted  | 5 stacks of wheat | Order Fulfilment/Delivery  | 7 |

### Cheese
Cheese is a crafted resource needed for fulfilling recipes asked for through customer/tourist scenario interactions. 
Cheese can be crafted using milk gathered from barn structures. 

|Name in Game   | Type of Resource  |  Ingredients needed |  Used for | Base Selling Price   |
|---|---|---|---|---|
| Cheese  | Crafted  | 1 bottle of milk | Order Fulfilment/Delivery  | 15 |

## Structures

### Player Home
Purely aesthetic structure that represents the player's home. 

| Name in Game  | Type of Structure  | Initial Cost  | Number of Upgrades  | Cost Per Upgrade  | Unlockable Through  |  Used to   | Crafting Duration  |
|---|---|---|---|---|---|---|---|
| Home  |  Decor | N/A  | 3  | 1000 x player level  | Completely Unlocked | N/A  | N/A  |


### Towny Home
Building more town homes allows for a higher citizen amount. This directly is responsible for improving production time through citizens.  

| Name in Game  | Type of Structure  | Initial Cost  | Number of Upgrades  | Cost Per Upgrade  | Unlockable Through  |  Used to   | Crafting Duration  |
|---|---|---|---|---|---|---|---|
| Town House  |  Town Improvement |  200 | 2  | 25 x player level  | initially unlocked but upgrades researched | adds a citizen  | N/A  |


### Farm Plot
Each harvest from a farm plot provides 1 + the number of upgrades stacks of what ever primitive resource was grown. Building more
farm plots allows for more resource gathering. 

| Name in Game  | Type of Structure  | Initial Cost  | Number of Upgrades  | Cost Per Upgrade  | Unlockable Through  |  Used to  | Crafting Duration  |
|---|---|---|---|---|---|---|---|
| Farm Plot  | Provide | 100  | unlimited  | 10 x player level  | Completely Unlocked  | Provide Plant Primitive Resources such as wheat/tomato  |   |


### Barn
Barns provide 1 bottle of milk every x duration. Crafting more barns allows for more resource gathering. Each upgrade provides a larger milk production. 

| Name in Game  | Type of Structure  | Initial Cost  | Number of Upgrades  | Cost Per Upgrade  | Unlockable Through  |  Used to   | Crafting Duration  |
|---|---|---|---|---|---|---|---|
| Cow Barn  | Provide | 450 | 2  | 300 x player level  | initially unlocked but upgrades researched | provide primitive resource milk  |   |


### Hospital
One of the initial starting structures, the hospital is responsible for providing blood. The player can run a "blood raid" every x duration for a randomly generated chance of filling the blood inventory. Upgrades raise the number of based guaranteed blood to be added. Players can **not** build/purchase multiple hospitals. 

| Name in Game  | Type of Structure  | Initial Cost  | Number of Upgrades  | Cost Per Upgrade  | Unlockable Through  |  Used to   | Crafting Duration  |
|---|---|---|---|---|---|---|---|
| Medical Center  | Provide  | N/A  | 2 | 1500 x upgrade level  | initially unlocked but upgrades researched  | provide blood  |   |


### Flour Facility
Structure that takes 5 stacks of wheat to produce 1 flour crafted every x duration. Building more flour facilities allows for more crafting resources. 

| Name in Game  | Type of Structure  | Initial Cost  | Number of Upgrades  | Cost Per Upgrade  | Unlockable Through  |  Used to   | Crafting Duration  |
|---|---|---|---|---|---|---|---|
| Windmill  | Refine  | 500 | 3  | 325 x player level  | collection of 10 stacks of wheat  | refine primitive resource wheat into crafted resource flour  |   |


### Cheese Facility
Structure that takes 1 bottle of milk to produce 1 cheese crafted every x duration. Building more cheese facilities allows for more crafting resources. 

| Name in Game  | Type of Structure  | Initial Cost  | Number of Upgrades  | Cost Per Upgrade  | Unlockable Through  |  Used to  | Crafting Duration  |
|---|---|---|---|---|---|---|---|
| Coagulation Plant  | Refine  | 500 | 3  | 325 x player level  | collection of 5 milk bottles  | refine primitive resource milk in to crafted resource cheese  |   |


### Research Facility
Special **unique** structure that can only research new upgrades or resources over a duration. Will take a one time purchase for each deployed "research project". 

| Name in Game  | Type of Structure  | Initial Cost  | Number of Upgrades  | Cost Per Upgrade  | Unlockable Through  |  Used to   | Crafting Duration  |
|---|---|---|---|---|---|---|---|
| Center of Science  | Town Improvement  | 1000  | 0 | N/A  | Fufilling at least 10 orders  | improve Unlockable Content  |   |


### Storage Facility
Special structure that can be built to improve inventory size. There is a max limit of 5 built storage facilities allowed. Each upgrade increases the initial upgrade value of 10 by another 10. 

| Name in Game  | Type of Structure  | Initial Cost  | Number of Upgrades  | Cost Per Upgrade  | Unlockable Through  |  Used to   | Crafting Duration  |
|---|---|---|---|---|---|---|---|
| Storage Center  | Town Improvement  | 1000  | unlimited  | 1000 x upgrade level  | Completely Unlocked | add more inventory space  | N/A  |

# Progression
## Pizza Orders
Customers/Tourists will confront the player with orders. Many of the orders are wacky and potentially 
resource heavy. Each fufilled order has valuable rewards such as unlocks, experience, money, or special events. 
