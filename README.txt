README V2 - Design Patterns Lab 10/27/2024

Save System

Scripts: ISaveable, SaveLoadInput, TransformSaver, PlayerTransformSaver, ScoreSaver

ISaveable contains two interfaces: ISaveable, which stores Json formatted data, and ISaveableBin, which stores .bin formatted data. ISaveable uses a class called SaveData to store player position and enemy information, with each enemy instance being stored within a list, found in the SaveDataContainer class. ScoreData will be written as a .bin file and stores only the player's score, which requires only one instance.

ISaveable also contains the static SavingService method, which contains save and load methods for json and .bin files. Each save method searches through objects containing their respective interfaces within the scene, storing this data and writing it into a save file. Each load method searches for the data path in which the save data is located and deserializes the data, calling each interface inheritor's LoadFromData method afterwards.

SaveLoadInput stores the file name of each save file. It also tracks "s" and "l" key inputs for calling SavingService's saving and loading methods respectively.

The three saver files define keys for their respective objects and use them to read the correct data from save files. As such, they must also be components of their respective objects within the scene; TransformSaver for enemies, PlayerTransformSaver for the player, and ScoreSaver for the Score Manager. These scripts inherit from their respective interfaces and define methods that both save and load specific pieces of data using SavingService's methods. Finally, this data is sent back into the objects' scripts and components for use within the scene.

README V1 - Design Patterns Lab 10/20/2024

Object Pool

Scripts: BulletPool, StandardBullet, IBullet, PlayerShooting

	IBullet is an interface containing methods pertaining to any bullet object.
StandardBullet inherits from this, defining actions such as movement to/from the shooter's position and adding/removing force onto and from a bullet.
When a bullet collides with something or leaves the screen, it returns to its original position within the pool.
Each bullet contains a StandardBullet script, as it contains the core properties of individual bullets.

	BulletPool contains the object pool of bullets, storing each child of the object it is attached to within a list.
This script controls the process of finding an existing bullet object, which is stored within the list, and provides shooting functionality when found.
Shooting functionality works by calling a method from the individual bullet found.

	PlayerShooting is simply what provides the input to call Shoot from BulletPool.


Observer

Scripts: ScoreManager, Enemy1

	ScoreManager keeps track of every Enemy1 scripts, listening for their death event. Once dead, ScoreManager intakes their point value and updates the score value with it, which updates the on-screen UI as well.


Builder

Scripts: Builder, EnemyBehaviour, Shop, Enemy, EnemyBuilder, LargeEnemy, MediumEnemy, SmallEnemy

	Enemy is the base class for an enemy. It contains the enemy's type, as well as its speed, color, and score values.

	EnemyBuilder is an abstract class that contains methods for modifying the enemy's various traits, each of which are defined by inheritors.

	LargeEnemy, MediumEnemy, and SmallEnemy inherit from EnemyBuilder and use its methods to modify the base Enemy class' traits. Admittedly, their names are a bit 
misleading, as Large, Medium, and Small do not refer to their size, but instead their speed; LargeEnemy moves slowly, MediumEnemy moves moderately, and SmallEnemy moves quickly.

	Shop is a class that "constructs" each enemy by calling the methods they inherit from EnemyBuilder.

	Builder and EnemyBehaviour both inherit from MonoBehaviour. Builder does two things: construct each type of enemy and instantiate them into the scene. It accomplishes the first task by creating new instances of each enemy type, using an instance of shop to call their construct methods, and adding them to a list of enemy types. The second task is accomplished by creating an enemy GameObject in the scene on a timer, adding an EnemyBehaviour component to it, and assigning it values based on one of three enemy types.

	EnemyBehaviour puts an enemy's traits to use during runtime. It takes the values given to it by Builder and uses them to assign an enemy a movement speed, sprite color, and score value passed whenever it is destroyed by the player. This script is the final result of the enemy builder system.


