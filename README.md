<div align = "center"> 
  <a href="https://gokolokgjo.itch.io/iron-covenant">
    <img src="photoes for GitHub/LogoV8.png" width="400" border = "4">
  </a>
</div>

<h1 align = "center"> Iron Covenant </h1> 

<h3>
  Get ready to face to face with increasingly dangerous waves of monsters. 
  <br> Choose your character and upgrades and try to elliminate each of the beasts on your road.
</h3> 

<h2 align = "center">Overview</h2>

Iron covenant is a game created with Unity Engine to learn and demonstrate gameplay programming skill.

### While developing I have focused on:

* Data-driven system
* Pooling system
* Two layer-upgrade system (Permanent and on run selectable)
* Runtime Effect interactions
* State systems
* Structurization assets

<h2 align = "center"> Core features</h2>
<ul> 
  <li> <a href = "#Technical-Features"> Technical features </a> 
    <ul> 
      <li> <a href = "#SO-DrivenArchtitecture">Scriptable object-driven achitecture</a> </li>
      <li> <a href = "#Upgrade-system">Two layers upgrade system</a> </li>
      <li> <a href = "#Object-Pooling">Object Pooling</a> </li>
      <li> <a href = "#Combat-System"> Combat System</a></li>
      <li> <a href = "#State-System"> State System</a></li>
    </ul>
  </li>
  <li> <a href = "#Challenges"> Challenges </a></li>
  <li> <a href = "№Future-Plans"> Technogies Plans</a> </li>
  <li> <a href = "№Controls"> Controls</a> </li>
  <li> <a href = "№Technogies-Used"> Technogies Used</a> </li>
  <li> <a href = "№Credits"> Credits</a> </li>
</ul>

<a id = "Technical-Features"></a>
<h2 align = "center"> Technical features</h2>

<a id = "SO-DrivenArchtitecture" ></a>
<h3 >Scriptable object-driven achitecture  </h3>

<a id = "Definition-SO"  ></a>
<h4 align = "right"><em > from here SO - Scriptable object </em></h4> 


I have used SO assets to be able to edit and save data between scenes. It gives ability to reuse data for all exemplaires of the certain class.

Also I have used SO for Save Manager to avoid wide spreaded usage of Singletones and to avoid Null References due to Life cycles of Singleton.


<ul>
  <li> <b>UpgradeSystem based on one SO </b> (
    <a href = "Assets/Domains/UpgradeSystem/scripts/Upgrades/UpgradeSO.cs">Upgrade SO</a>
    )
  </li>
  <li> Enemy system based on SOs for basic stats and spawn (
    <a href = "Assets/Domains/Entity/scripts/Enemy/SO/BasicStatsEnemySO.cs">Enemy Stats SO</a>,
    <a href = "Assets/Domains/Entity/scripts/Enemy/SpawnManager/EnemySpawnSO.cs">Enemy Spawn Base SO</a>
    )
  </li>
  <li> Weapon based on SOs for basic stats (
    <a href = "Assets/Domains/Weapon/scripts/SO/WeaponStatsSO.cs">Weapon Stats SO</a>
    )
  </li>
  <li> Character and map choices realized based on SOs (
    <a href = "Assets/Domains/GameSelectSystem/scripts/SO/PlayerChoicesSO.cs">Character choice SO</a>,
    <a href = "Assets/Domains/GameSelectSystem/scripts/SO/MapChoicesSO.cs">Map choice SO</a>
    )
  </li>
  <li> Music base (Music list for each arena) is based on SO (
    <a href = "Assets/Domains/MusicSystem/scripts/So/MusicBaseSO.cs">Music Base SO</a>
    )
  </li>
  <li> Save manager is based on SO (
    <a href = "Assets/Domains/SaveSystem/scripts/Manager/SaveManagerSO.cs" >Save Manager SO</a>
    )
  </li>
</ul>

<a id = "Upgrade-system"></a>
<h3> Two layers Upgrade system </h3>

I have realized Upgrade system with two layers of purchasing: Permanent and Selectable.

* Permanent Upgrades are being purchased in main menu between runs.
* Selectable Upgrades are being purchased in run when level ups.

<p>Each of them uses the same <a href = "#Definition-SO">SO</a></p>
<br>
Upgrades include effects for weapon and stats modifiers. It gives an ability to merge systems havind only one source of data. The purpuse of certain Upgrade is being set by enum list

    public enum TypeOfAddedUpgrade 
    { 
      statsModifier,
      newEffect
    } 
    public class UpgradeSO : ScriptableObject
    {
    ...
    [SerializeField] private List<TypeOfAddedUpgrade> _upgradeTypes;
    ...
    }


Upgrades are being calculated separately and they are being merged while being used.

<a id = "Enemy-system"></a>
<h3> Enemy system </h3>

* Enemy: 
  * Enemies use SOs for basic stats.
  * Enemies with type of Boss can give specific regard (new character, new map).
* Enemy spawner:
  * Enemy spawner referes to pool has base of enemies prefabs, their spawn intervals, and type of spawned enemy (regular or boss).
 
        private void SpawnEnemy()
          {
            ...
            int randomAmountOfEnemy = Random.Range(0, randomAmountLimit + 1);
            for (int i = 0; i < randomAmountLimit; i++) 
              {
                Enemy enemy = _enemyPool.GetEnemy(out ObjectPool<Enemy> pool);
                if (enemy != null)
                  {
                    enemy.Initialize(pool, GetSpawnPosition());
                  }
              }
          }

  
    * type of enemy determines amount of created exemplaires of enemy prefab in pool.
  * Enemies are being spawned in random radius that is clamped in set interval.
  * Spawn area is limited by arena tile map.
 
        const int maxAttempts = 50;

        for (int i = 0; i < maxAttempts; ++i)
        {
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float distance = Random.Range(_minSpawnRadius, _maxSpawnRadius);
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            Vector2 spawnPosition = (Vector2)_player.transform.position + direction * distance;
            Vector3Int cellPos = _arenaTileMap.WorldToCell(spawnPosition);
            BoundsInt bounds = _arenaTileMap.cellBounds;
            if (!_arenaTileMap.HasTile(cellPos))
            {
                return spawnPosition;
            }
        }

<a id = "Object-Pooling"></a>
<h3> Object Pooling </h3>

I used object pooling to decrease Garbage Collection load. It's very useful tool for game-creating, especially when a lot of same game objects. are being initialized and destroyed.

I have used it for next systems:

* Projectiles
* Enemies
* Damage display panels
* Collectable exp gems

I have created my own pool system based om generic class:

    public class ObjectPool<T> where T : Component
    {
    ...
    }

I know that Unity Engine already know its own tool for pooling, but I wanted to practise in Generic Classes and to understand how it works inside.

<a id = "Combat-System"></a>
<h3>Combat System</h3>

Each character has certain weapon. Each weapon inherits abstract class of Weapon that is keeping basic stats, required references and abstract methods.

For now there are two characters with two conceptually different weapons. they both works not with amount of damage, but with damage data class that includes damage type, amount, flags to ignore defence or invincibility and source of damage. Each of them has two attack types (regular and ability):

* knight with a sword:
  * Melle attack works with animation clip events. They are activating and disactivating collider of attack area.
  * it has splash damage attacking everyone who is in attack collider.
  * ability is just an attack with bigger area and bigger damage.
* Gunner with a gun
  * Range attack works on getting projectile from pool and initializing it.
    * Each projectile has it's own stats, that are being set in initialization (damage data, penetration, speed, life time)
    * Projectile has its own collider with trigger-on;
  * ability attack is shooting ability-projectile from another pool. It has infinite penetration and it's own stats.

Both use collider to interact with any entity that has class inherite from damageble interface (Idamagable). 
Basic stats of weapon define who is the trigger by layer mask.

<a id = "State-System"></a>
<h3>State System</h3>

I have realized Global State System that is based in singleton patern. 

It keeps current state and calls event to notify each subscibed class that state has been changed.

it's very useful for scaling prject. You don't need to disable or enable some features directly when it's required - you can just change state and reaalize diabling for features that are needed to be disabled.

Also I realized Enemy State System. It's also very useful for scaling enemy system for many states. Behavior depends on certain state (chase, idle, patrol)


<h2 id = "Challenges" align = "center" >Challenges</h2>

<h3> Large fps drops killing enemies</h3>

I have troubles while i was testing when a lot of gameobjects were destroying fps dropped. 

Solution is Pooling or reusing game objects.
It gived me stable performance even when it's hundreds of enemies.

<h3>Structure issues </h3>

While I was creating my game i had a lot of time wasting on searching through folders in extension-oriented structure.

Solution is domain-oriented structure. It's ergonomic when everything that is a part of certain system is nearby. It saves a lot of time.

<h3> Upgrade scaleability</h3>

At first I created Upgrade system where each upgrade layer had had its own Upgrade SO. It was so hard to scale.

Solution is to rework for one SO. It has became easy to merge.

<a align = "center" id = "Future-Plans"></a>
<h2> Future Plans </h2>

I'm going to extend my game:

* improve enemy ai
* improve audio
* impove enemy archtitecture
* add new characters with new specific weapon.
* add mew maps
* add new effects
* add new upgrades
* add new enemies
* add new animations
* rework menu
  
<a align = "center" id = "Controls"></a>
<h2> Controls </h2>
  WASD - Movement
  Mouse - Aim
  LMB - Attack
  RMB - Ability
  ESC - Pause
  Tab - Pause (WEBGL-build)

<a align = "center" id = "Technogies-Used"></a>
<h2>Technogies Used</h2>

* Unity 6
* C#
* Git
* URP

<a  id = "Credits"></a>
<h2 align = "center"> Credits </h2>

Programing: Roman Petrovskyi
Assets : [environment](https://assetstore.unity.com/packages/2d/environments/pixel-art-top-down-basic-187605)




