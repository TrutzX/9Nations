
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Data{
	//Document URL: https://spreadsheets.google.com/feeds/worksheets/1U663Qd5fqg1MNDhFQ6uvzYjlmshHd7Yyk8YIkib1CbQ/public/basic?alt=json-in-script

	//Sheet SheetRess
	public static DataTypes.SheetRess ress = new DataTypes.SheetRess();
	//Sheet SheetElement
	public static DataTypes.SheetElement element = new DataTypes.SheetElement();
	//Sheet SheetNAction
	public static DataTypes.SheetNAction nAction = new DataTypes.SheetNAction();
	//Sheet SheetUnit
	public static DataTypes.SheetUnit unit = new DataTypes.SheetUnit();
	//Sheet SheetNTerrain
	public static DataTypes.SheetNTerrain nTerrain = new DataTypes.SheetNTerrain();
	//Sheet SheetNation
	public static DataTypes.SheetNation nation = new DataTypes.SheetNation();
	//Sheet SheetBuilding
	public static DataTypes.SheetBuilding building = new DataTypes.SheetBuilding();
	//Sheet SheetHelp
	public static DataTypes.SheetHelp help = new DataTypes.SheetHelp();
	static Data(){
		ress.Init(); element.Init(); nAction.Init(); unit.Init(); nTerrain.Init(); nation.Init(); building.Init(); help.Init(); 
	}
}


namespace DataTypes{
	public partial class Ress{
		public string id;
		public string name;
		public string sound;
		public string icon;
		public int market;
		public float storage;
		public int rechangeatdestroy;

		public Ress(){}

		public Ress(string id, string name, string sound, string icon, int market, float storage, int rechangeatdestroy){
			this.id = id;
			this.name = name;
			this.sound = sound;
			this.icon = icon;
			this.market = market;
			this.storage = storage;
			this.rechangeatdestroy = rechangeatdestroy;
		}
	}
	public class SheetRess: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,9,3,11,24,6);
		public readonly string[] labels = new string[]{"id","name","sound","icon","int market","float storage","int rechangeatdestroy"};
		private Ress[] _rows = new Ress[37];
		public void Init() {
			_rows = new Ress[]{
					new Ress("wood","Wood","interface5","base:wood",4,1f,50),
					new Ress("plank","Plank","","base:plank",12,1f,50),
					new Ress("stone","Stone","metalPot3","base:stone",6,1f,50),
					new Ress("brick","Brick","","base:brick",18,1f,50),
					new Ress("ore","Ore","","base:ore",5,1f,50),
					new Ress("tool","Tool","","base:tool",14,1f,50),
					new Ress("weapon","Weapon","","base:weapon",54,1f,50),
					new Ress("food","Food","bite-small3","base:food",1,0.5f,50),
					new Ress("gold","Gold","","other:gold",5,0.25f,50),
					new Ress("worker","Worker","","ui:worker",0,0f,100),
					new Ress("workermax","Worker","","ui:workermax",0,0f,100),
					new Ress("research","Research","","382",0,0f,50),
					new Ress("faith","Faith","","528",0,0f,50),
					new Ress("safety","Safety","","329",0,0f,50),
					new Ress("culture","Culture","","80",0,0f,50),
					new Ress("wealth","Wealth","","147",0,1f,50),
					new Ress("leaf","Leaf","","181",2,0.5f,50),
					new Ress("rope","Rope","","215",12,1f,50),
					new Ress("slingshot","Slingshot","","114",54,1f,50),
					new Ress("turnip","Turnip","","718",0,1f,50),
					new Ress("carrot","Carrot","","734",0,1f,50),
					new Ress("cabbage","Cabbage","","750",0,1f,50),
					new Ress("potato","Potato","","766",0,1f,50),
					new Ress("onion","Onion","","782",0,1f,50),
					new Ress("corn","Corn","","798",0,1f,50),
					new Ress("tomato","Tomato","","814",0,1f,50),
					new Ress("wheat","Wheat","","830",0,1f,50),
					new Ress("bellpepper","Bell pepper","","719",0,1f,50),
					new Ress("eggplant","Eggplant","","735",0,1f,50),
					new Ress("cauliflower","Cauliflower","","751",0,1f,50),
					new Ress("broccoli","Broccoli","","767",0,1f,50),
					new Ress("pumpkin","Pumpkin","","783",0,1f,50),
					new Ress("cucumber","Cucumber","","799",0,1f,50),
					new Ress("strawberry","Strawberry","","815",0,1f,50),
					new Ress("peanut","Peanut","","831",0,1f,50),
					new Ress("seed","Seed","","247",15,1f,50),
					new Ress("buildtime","Buildtime","","base:build",0,0f,0)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetRess t;
			public SheetEnumerator(SheetRess t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		public int Length{ get{ return _rows.Length; } }
		public Ress this[int index]{
			get{
				return _rows[index];
			}
		}
		public Ress this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].id == id){ return _rows[i]; }
				}
				return null;
			}
		}
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].id == key){ return true; }
			}
			return false;
		}
		public Ress[] ToArray(){
			return _rows;
		}
		public Ress Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public Ress wood{	get{ return _rows[0]; } }
		public Ress plank{	get{ return _rows[1]; } }
		public Ress stone{	get{ return _rows[2]; } }
		public Ress brick{	get{ return _rows[3]; } }
		public Ress ore{	get{ return _rows[4]; } }
		public Ress tool{	get{ return _rows[5]; } }
		public Ress weapon{	get{ return _rows[6]; } }
		public Ress food{	get{ return _rows[7]; } }
		public Ress gold{	get{ return _rows[8]; } }
		public Ress worker{	get{ return _rows[9]; } }
		public Ress workermax{	get{ return _rows[10]; } }
		public Ress research{	get{ return _rows[11]; } }
		public Ress faith{	get{ return _rows[12]; } }
		public Ress safety{	get{ return _rows[13]; } }
		public Ress culture{	get{ return _rows[14]; } }
		public Ress wealth{	get{ return _rows[15]; } }
		public Ress leaf{	get{ return _rows[16]; } }
		public Ress rope{	get{ return _rows[17]; } }
		public Ress slingshot{	get{ return _rows[18]; } }
		public Ress turnip{	get{ return _rows[19]; } }
		public Ress carrot{	get{ return _rows[20]; } }
		public Ress cabbage{	get{ return _rows[21]; } }
		public Ress potato{	get{ return _rows[22]; } }
		public Ress onion{	get{ return _rows[23]; } }
		public Ress corn{	get{ return _rows[24]; } }
		public Ress tomato{	get{ return _rows[25]; } }
		public Ress wheat{	get{ return _rows[26]; } }
		public Ress bellpepper{	get{ return _rows[27]; } }
		public Ress eggplant{	get{ return _rows[28]; } }
		public Ress cauliflower{	get{ return _rows[29]; } }
		public Ress broccoli{	get{ return _rows[30]; } }
		public Ress pumpkin{	get{ return _rows[31]; } }
		public Ress cucumber{	get{ return _rows[32]; } }
		public Ress strawberry{	get{ return _rows[33]; } }
		public Ress peanut{	get{ return _rows[34]; } }
		public Ress seed{	get{ return _rows[35]; } }
		public Ress buildtime{	get{ return _rows[36]; } }

	}
}
namespace DataTypes{
	public partial class Element{
		public string id;
		public string name;
		public int icon;

		public Element(){}

		public Element(string id, string name, int icon){
			this.id = id;
			this.name = name;
			this.icon = icon;
		}
	}
	public class SheetElement: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,9,3,11,24,7);
		public readonly string[] labels = new string[]{"id","name","icon"};
		private Element[] _rows = new Element[9];
		public void Init() {
			_rows = new Element[]{
					new Element("fire","Fire",64),
					new Element("air","Air",69),
					new Element("death","Death",1),
					new Element("earth","Earth",543),
					new Element("energy","Energy",66),
					new Element("life","Life",70),
					new Element("metal","Metal",88),
					new Element("water","Water",67),
					new Element("wood","Wood",295)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetElement t;
			public SheetEnumerator(SheetElement t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		public int Length{ get{ return _rows.Length; } }
		public Element this[int index]{
			get{
				return _rows[index];
			}
		}
		public Element this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].id == id){ return _rows[i]; }
				}
				return null;
			}
		}
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].id == key){ return true; }
			}
			return false;
		}
		public Element[] ToArray(){
			return _rows;
		}
		public Element Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public Element fire{	get{ return _rows[0]; } }
		public Element air{	get{ return _rows[1]; } }
		public Element death{	get{ return _rows[2]; } }
		public Element earth{	get{ return _rows[3]; } }
		public Element energy{	get{ return _rows[4]; } }
		public Element life{	get{ return _rows[5]; } }
		public Element metal{	get{ return _rows[6]; } }
		public Element water{	get{ return _rows[7]; } }
		public Element wood{	get{ return _rows[8]; } }

	}
}
namespace DataTypes{
	public partial class NAction{
		public string id;
		public string name;
		public string desc;
		public int cost;
		public string icon;
		public bool active;
		public string req1;
		public string req2;
		public bool useUnderConstruction;

		public NAction(){}

		public NAction(string id, string name, string desc, int cost, string icon, bool active, string req1, string req2, bool useUnderConstruction){
			this.id = id;
			this.name = name;
			this.desc = desc;
			this.cost = cost;
			this.icon = icon;
			this.active = active;
			this.req1 = req1;
			this.req2 = req2;
			this.useUnderConstruction = useUnderConstruction;
		}
	}
	public class SheetNAction: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,9,3,11,24,7);
		public readonly string[] labels = new string[]{"id","name","desc","cost","icon","active","req1","req2","useUnderConstruction"};
		private NAction[] _rows = new NAction[9];
		public void Init() {
			_rows = new NAction[]{
					new NAction("destroy","Destroy it","Destroy it",0,"base:destroy",true,"","",true),
					new NAction("build","Build","Construct a new Building",5,"base:build",true,"empty:building","townMin:1",false),
					new NAction("foundTown","Found a new town","Found a new town",7,"base:foundTown",true,"","",false),
					new NAction("train","Train","Train a new unit",0,"base:train",true,"empty:unit","",false),
					new NAction("buildUpgrade","Upgrade","Upgrade this building",0,"icons:upgrade",true,"","",false),
					new NAction("trainUpgrade","Upgrade","Upgrade this unit",5,"icons:upgrade",true,"","",false),
					new NAction("endGameLose","Lose","Lose the game",0,"base:destroy",true,"","",true),
					new NAction("endGameWin","Win","Win the game",0,"other:win",true,"","",true),
					new NAction("trade","Trade","Trade your ressources",2,"icons:trade",true,"townMin:1","",false)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetNAction t;
			public SheetEnumerator(SheetNAction t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		public int Length{ get{ return _rows.Length; } }
		public NAction this[int index]{
			get{
				return _rows[index];
			}
		}
		public NAction this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].id == id){ return _rows[i]; }
				}
				return null;
			}
		}
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].id == key){ return true; }
			}
			return false;
		}
		public NAction[] ToArray(){
			return _rows;
		}
		public NAction Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public NAction destroy{	get{ return _rows[0]; } }
		public NAction build{	get{ return _rows[1]; } }
		public NAction foundTown{	get{ return _rows[2]; } }
		public NAction train{	get{ return _rows[3]; } }
		public NAction buildUpgrade{	get{ return _rows[4]; } }
		public NAction trainUpgrade{	get{ return _rows[5]; } }
		public NAction endGameLose{	get{ return _rows[6]; } }
		public NAction endGameWin{	get{ return _rows[7]; } }
		public NAction trade{	get{ return _rows[8]; } }

	}
}
namespace DataTypes{
	public partial class Unit{
		public string id;
		public string name;
		public string file;
		public string cat;
		public string movetyp;
		public int atk;
		public int def;
		public int dam_min;
		public int dam_max;
		public int hp;
		public int ap;
		public int visible;
		public int buildtime;
		public int reqbuildtownlevel;
		public string reqbuildnation;
		public string reqbuild1;
		public string reqbuild2;
		public string reqbuild3;
		public string cost1;
		public string cost2;
		public string cost3;
		public string cost4;
		public string cost5;
		public string cost6;
		public string cost7;
		public string reqproduce1;
		public string reqproduce2;
		public string reqproduce3;
		public string produce1;
		public string produce2;
		public string produce3;
		public string research;
		public string action1;
		public string action2;
		public string action3;
		public string action4;
		public string action5;
		public int embark;
		public string actproduce;
		public string aproduce;
		public int gift;
		public string build;
		public string foundTown;
		public string reqdate;
		public int actionTerrainRemoveforest;
		public int actionTerrainAddgrass;
		public string actionalchemyA;

		public Unit(){}

		public Unit(string id, string name, string file, string cat, string movetyp, int atk, int def, int dam_min, int dam_max, int hp, int ap, int visible, int buildtime, int reqbuildtownlevel, string reqbuildnation, string reqbuild1, string reqbuild2, string reqbuild3, string cost1, string cost2, string cost3, string cost4, string cost5, string cost6, string cost7, string reqproduce1, string reqproduce2, string reqproduce3, string produce1, string produce2, string produce3, string research, string action1, string action2, string action3, string action4, string action5, int embark, string actproduce, string aproduce, int gift, string build, string foundTown, string reqdate, int actionTerrainRemoveforest, int actionTerrainAddgrass, string actionalchemyA){
			this.id = id;
			this.name = name;
			this.file = file;
			this.cat = cat;
			this.movetyp = movetyp;
			this.atk = atk;
			this.def = def;
			this.dam_min = dam_min;
			this.dam_max = dam_max;
			this.hp = hp;
			this.ap = ap;
			this.visible = visible;
			this.buildtime = buildtime;
			this.reqbuildtownlevel = reqbuildtownlevel;
			this.reqbuildnation = reqbuildnation;
			this.reqbuild1 = reqbuild1;
			this.reqbuild2 = reqbuild2;
			this.reqbuild3 = reqbuild3;
			this.cost1 = cost1;
			this.cost2 = cost2;
			this.cost3 = cost3;
			this.cost4 = cost4;
			this.cost5 = cost5;
			this.cost6 = cost6;
			this.cost7 = cost7;
			this.reqproduce1 = reqproduce1;
			this.reqproduce2 = reqproduce2;
			this.reqproduce3 = reqproduce3;
			this.produce1 = produce1;
			this.produce2 = produce2;
			this.produce3 = produce3;
			this.research = research;
			this.action1 = action1;
			this.action2 = action2;
			this.action3 = action3;
			this.action4 = action4;
			this.action5 = action5;
			this.embark = embark;
			this.actproduce = actproduce;
			this.aproduce = aproduce;
			this.gift = gift;
			this.build = build;
			this.foundTown = foundTown;
			this.reqdate = reqdate;
			this.actionTerrainRemoveforest = actionTerrainRemoveforest;
			this.actionTerrainAddgrass = actionTerrainAddgrass;
			this.actionalchemyA = actionalchemyA;
		}
	}
	public class SheetUnit: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,9,3,11,24,8);
		public readonly string[] labels = new string[]{"id","name","file","cat","movetyp","atk","def","dam_min","dam_max","hp","ap","visible","buildtime","req-build-townlevel","req-build-nation","req-build1","req-build2","req-build3","cost1","cost2","cost3","cost4","cost5","cost6","cost7","req-produce1","req-produce2","req-produce3","produce1","produce2","produce3","research","action1","action2","action3","action4","action5","embark","act-produce","aproduce","gift","build","foundTown","req-date","actionTerrainRemove-forest","actionTerrainAdd-grass","action-alchemyA"};
		private Unit[] _rows = new Unit[27];
		public void Init() {
			_rows = new Unit[]{
					new Unit("nsettler","Settler","nsettler","prod","walk",0,5,1,3,10,5,0,9,3,"north","","","","wood:15","food:60","stone:8","tool:10","gold:2","worker:6","","ressMin:wood,1","ressMin:food,1","","wood:-1","food:-1","","air,wood,life","","","","","",0,"food-2","",0,"","1","",0,0,""),
					new Unit("nworker","Worker","nworker","prod","walk",0,5,1,3,10,15,-4,1,0,"north","","","","wood:2","food:5","","","","worker:1","","","","","","","","","destroy","build","","","",0,"wood-3","stone-3",0,"1","","",1,0,""),
					new Unit("nship","Ship","nship","explo","swim",6,5,2,3,10,20,2,6,3,"north","","","","plank:10","","tool:3","","","worker:2","","","","","","","","","","","","","",2,"","",0,"bridge","","",0,0,""),
					new Unit("nexplorer","Explorer","nexplorer","explo","walk",6,3,2,3,10,10,3,3,1,"north","","","","wood:2","food:10","stone:1","","","worker:1","","","","","","","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("nmarksman","Marksman","","","walk",6,3,2,3,10,6,1,1,1,"north","","","","","","","","","","","","","","","","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("nsoldier","Swordsman","nsoldier","war","walk",10,12,6,9,35,10,2,6,3,"north","","","","","","weapon:1","","gold:2","worker:1","","","ressMin:food,1","","","food:-1","","metal,death","destroy","","","","",0,"","",0,"","","",0,0,""),
					new Unit("nmonk","Monk","nmonk","","walk",12,7,10,12,30,5,1,1,3,"north","","","","","","","","","","","","","","","","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("nking","King","nking","general","walk",12,10,10,12,30,7,1,1,1,"north","maxUnitPlayer:nking,0","","","","","","","","","","","","","","","","","build","foundTown","","","",0,"food-3","",0,"1","first","",0,0,""),
					new Unit("ngeneral","General","ngeneral","","walk",15,15,15,25,100,5,1,1,4,"north","","","","","","","","","","","","","","","","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("nmage","Mage","nmage","war","walk",16,16,20,25,100,9,2,4,4,"north","","","","","","weapon:2","","gold:6","worker:1","","","ressMin:food,2","","","food:-2","","fire,death,energy","","","","","",0,"","",0,"","","",0,0,"gold-4"),
					new Unit("rworker","Worker","rgnom","prod","walk",0,3,2,3,8,10,0,1,1,"ranger","","","","wood:2","food:5","tool:2","","","worker:1","","","","","","","","","","","","","",0,"wood-3","leaf-3",0,"1","","",0,1,""),
					new Unit("rexplorer","Explorer","units1.png-4","explo","fly",6,4,2,3,10,15,3,3,1,"ranger","","","","wood:2","food:10","weapon:5","","gold:1","worker:1","","","","","","","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("rnature","Nature","units1.png-4","general","walk",6,7,2,4,20,5,2,1,1,"ranger","maxUnitPlayer:rnature,0","","","","","","","","","","","","","","","","","","","","","",0,"food-3","",0,"1","first","",0,1,""),
					new Unit("rfox","Fox","units1.png-91","war","walk",7,7,2,4,20,10,1,4,1,"ranger","","","","wood:2","food:6","tomato:1","","","","","","","","","","","wood","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("rgoldmonkey","Gold Monkey","units2.png-36","war","fly",18,18,40,50,180,10,1,2,4,"ranger","","","","","food:2","","leaf:3","","worker:1","","","","","","","","air,metal,energy","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("rmonkey","Monkey","units2.png-24","war","walk",9,5,3,5,15,6,1,8,2,"ranger","","","","","","peanut:1","leaf:6","","","","","","","","","","air,wood","trainUpgrade:rgoldmonkey","","","","",0,"","",0,"","","",0,0,""),
					new Unit("rrabbit","Rabbit","units1.png-76","prod","walk",9,5,3,5,15,7,1,5,2,"ranger","","","","","food:4","carrot:1","leaf:2","","","","","","","","","","earth,wood","","","","","",0,"food-5","",0,"","","",0,0,""),
					new Unit("rcornfairy","Corn Fairy","units1.png-46","prod","fly",15,14,18,22,90,7,2,10,4,"ranger","","","","","","corn:3","","","","","","","","","","","air,wood,life","trainUpgrade:rfairy","","","","",0,"","",0,"","","",0,0,""),
					new Unit("rfairy","Fairy","units1.png-15","war","fly",9,8,5,9,30,8,2,6,2,"ranger","","","","wood:15","food:15","tool:10","stone:1","gold:1","worker:1","seed:3","","ressMin:food,1","","","food:-1","","air,death","","","","","",0,"food-2","",0,"","","",0,0,""),
					new Unit("rslime","Slime","units2.png-19","war","swim",9,10,5,9,30,12,1,3,3,"ranger","","","","","","","rope:3","gold:1","","","","","","","","","water,air,life","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("rsnake","Snake","units1.png-81","war","walk",9,12,10,14,55,3,1,4,3,"ranger","","","","","","","","","","","","","","","","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("rgolm","Golm","units1.png-16","war","walk",9,12,10,14,65,20,2,9,3,"ranger","","","","","food:25","tool:3","stone:3","gold:3","worker:2","","","ressMin:food,2","","","food:-2","","wood,death,energy","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("relf","Elf","units1.png-6","prod","walk",15,14,18,22,110,9,2,12,2,"ranger","maxUnitPlayer:relf,0","","","","","","","gold:1","worker:1","","","","","","","","life","","","","","",0,"","",1,"","","357-361",0,0,""),
					new Unit("rpegasus","Pegasus","units1.png-56","war","fly",27,27,40,50,250,20,3,9,5,"ranger","","","","cucumber:2","","","rope:3","gold:1","worker:1","","","ressMin:gold,1","","","gold:-1","","air,wood,life","","","","","",1,"","",0,"","","",0,0,""),
					new Unit("dghost","Ghost","","","fly",7,7,3,5,18,5,0,5,0,"dead","","","","","","","","","","","","","","","","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("dnightking","Night King","units1.png-20","","WALK",13,10,11,15,40,7,0,10,0,"dead","maxUnitPlayer:dnightking,0","","","","","","","","","","","","","","","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("dblackknight","Black Knight","units2.png-52","","WALK",16,16,15,30,120,7,0,11,0,"dead","","","","","","","","","","","","","","","","","","","","","","",0,"","",0,"","","",0,0,"")
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetUnit t;
			public SheetEnumerator(SheetUnit t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		public int Length{ get{ return _rows.Length; } }
		public Unit this[int index]{
			get{
				return _rows[index];
			}
		}
		public Unit this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].id == id){ return _rows[i]; }
				}
				return null;
			}
		}
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].id == key){ return true; }
			}
			return false;
		}
		public Unit[] ToArray(){
			return _rows;
		}
		public Unit Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public Unit nsettler{	get{ return _rows[0]; } }
		public Unit nworker{	get{ return _rows[1]; } }
		public Unit nship{	get{ return _rows[2]; } }
		public Unit nexplorer{	get{ return _rows[3]; } }
		public Unit nmarksman{	get{ return _rows[4]; } }
		public Unit nsoldier{	get{ return _rows[5]; } }
		public Unit nmonk{	get{ return _rows[6]; } }
		public Unit nking{	get{ return _rows[7]; } }
		public Unit ngeneral{	get{ return _rows[8]; } }
		public Unit nmage{	get{ return _rows[9]; } }
		public Unit rworker{	get{ return _rows[10]; } }
		public Unit rexplorer{	get{ return _rows[11]; } }
		public Unit rnature{	get{ return _rows[12]; } }
		public Unit rfox{	get{ return _rows[13]; } }
		public Unit rgoldmonkey{	get{ return _rows[14]; } }
		public Unit rmonkey{	get{ return _rows[15]; } }
		public Unit rrabbit{	get{ return _rows[16]; } }
		public Unit rcornfairy{	get{ return _rows[17]; } }
		public Unit rfairy{	get{ return _rows[18]; } }
		public Unit rslime{	get{ return _rows[19]; } }
		public Unit rsnake{	get{ return _rows[20]; } }
		public Unit rgolm{	get{ return _rows[21]; } }
		public Unit relf{	get{ return _rows[22]; } }
		public Unit rpegasus{	get{ return _rows[23]; } }
		public Unit dghost{	get{ return _rows[24]; } }
		public Unit dnightking{	get{ return _rows[25]; } }
		public Unit dblackknight{	get{ return _rows[26]; } }

	}
}
namespace DataTypes{
	public partial class NTerrain{
		public string id;
		public string name;
		public string desc;
		public string sound;
		public int icon;
		public int walk;
		public int fly;
		public int swim;
		public int buildtime;
		public int visible;

		public NTerrain(){}

		public NTerrain(string id, string name, string desc, string sound, int icon, int walk, int fly, int swim, int buildtime, int visible){
			this.id = id;
			this.name = name;
			this.desc = desc;
			this.sound = sound;
			this.icon = icon;
			this.walk = walk;
			this.fly = fly;
			this.swim = swim;
			this.buildtime = buildtime;
			this.visible = visible;
		}
	}
	public class SheetNTerrain: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,9,3,11,24,9);
		public readonly string[] labels = new string[]{"id","name","desc","sound","icon","walk","fly","swim","buildtime","visible"};
		private NTerrain[] _rows = new NTerrain[18];
		public void Init() {
			_rows = new NTerrain[]{
					new NTerrain("border","Map Border","Not passable","",0,0,0,0,0,0),
					new NTerrain("fog","Fog","Not explored","",0,0,0,0,0,0),
					new NTerrain("unknown","Dark Terrain","Not visible","",394,0,0,0,0,0),
					new NTerrain("plains","Plains","default terrain","",0,5,5,0,0,0),
					new NTerrain("underground","Underground","default underground","",0,10,0,0,0,0),
					new NTerrain("tundra","Tundra","cold basic","",0,5,0,0,0,0),
					new NTerrain("sand","Sand","Hot basic","",0,5,0,0,0,0),
					new NTerrain("ocean","Ocean","","",16,0,10,10,0,0),
					new NTerrain("water","Fresh Water","","",0,0,5,5,0,0),
					new NTerrain("grass","Grass","","bite-small3",488,10,5,0,0,0),
					new NTerrain("woods","Woods","","interface5",104,10,5,0,25,-1),
					new NTerrain("hill","Hill","","",202,10,5,0,50,1),
					new NTerrain("mountain","Mountain","","metalPot3",204,15,7,0,100,2),
					new NTerrain("clouds","Clouds","","",0,0,0,0,0,0),
					new NTerrain("marsh","Marsh","","",0,15,5,10,0,0),
					new NTerrain("lava","Lava","","",0,0,0,0,0,0),
					new NTerrain("snow","Snow","","",0,0,0,0,0,0),
					new NTerrain("ice","Ice","","",0,0,0,0,0,0)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetNTerrain t;
			public SheetEnumerator(SheetNTerrain t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		public int Length{ get{ return _rows.Length; } }
		public NTerrain this[int index]{
			get{
				return _rows[index];
			}
		}
		public NTerrain this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].id == id){ return _rows[i]; }
				}
				return null;
			}
		}
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].id == key){ return true; }
			}
			return false;
		}
		public NTerrain[] ToArray(){
			return _rows;
		}
		public NTerrain Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public NTerrain border{	get{ return _rows[0]; } }
		public NTerrain fog{	get{ return _rows[1]; } }
		public NTerrain unknown{	get{ return _rows[2]; } }
		public NTerrain plains{	get{ return _rows[3]; } }
		public NTerrain underground{	get{ return _rows[4]; } }
		public NTerrain tundra{	get{ return _rows[5]; } }
		public NTerrain sand{	get{ return _rows[6]; } }
		public NTerrain ocean{	get{ return _rows[7]; } }
		public NTerrain water{	get{ return _rows[8]; } }
		public NTerrain grass{	get{ return _rows[9]; } }
		public NTerrain woods{	get{ return _rows[10]; } }
		public NTerrain hill{	get{ return _rows[11]; } }
		public NTerrain mountain{	get{ return _rows[12]; } }
		public NTerrain clouds{	get{ return _rows[13]; } }
		public NTerrain marsh{	get{ return _rows[14]; } }
		public NTerrain lava{	get{ return _rows[15]; } }
		public NTerrain snow{	get{ return _rows[16]; } }
		public NTerrain ice{	get{ return _rows[17]; } }

	}
}
namespace DataTypes{
	public partial class Nation{
		public string id;
		public string name;
		public bool hidden;
		public string desc;
		public int icon;
		public string leader;
		public string townhall;
		public string townlevel2;
		public string townlevel3;
		public string townlevel4;
		public float townLevelChangeablefood;
		public float townLevelChangeablegold;
		public int terrainforrest;
		public string origin;
		public string ethos;
		public string researchElement;
		public string hometerrain;
		public string townNameGenerator;
		public string townLevelName1;
		public string townLevelName2;
		public string townLevelName3;
		public string townLevelName4;

		public Nation(){}

		public Nation(string id, string name, bool hidden, string desc, int icon, string leader, string townhall, string townlevel2, string townlevel3, string townlevel4, float townLevelChangeablefood, float townLevelChangeablegold, int terrainforrest, string origin, string ethos, string researchElement, string hometerrain, string townNameGenerator, string townLevelName1, string townLevelName2, string townLevelName3, string townLevelName4){
			this.id = id;
			this.name = name;
			this.hidden = hidden;
			this.desc = desc;
			this.icon = icon;
			this.leader = leader;
			this.townhall = townhall;
			this.townlevel2 = townlevel2;
			this.townlevel3 = townlevel3;
			this.townlevel4 = townlevel4;
			this.townLevelChangeablefood = townLevelChangeablefood;
			this.townLevelChangeablegold = townLevelChangeablegold;
			this.terrainforrest = terrainforrest;
			this.origin = origin;
			this.ethos = ethos;
			this.researchElement = researchElement;
			this.hometerrain = hometerrain;
			this.townNameGenerator = townNameGenerator;
			this.townLevelName1 = townLevelName1;
			this.townLevelName2 = townLevelName2;
			this.townLevelName3 = townLevelName3;
			this.townLevelName4 = townLevelName4;
		}
	}
	public class SheetNation: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,9,3,11,24,9);
		public readonly string[] labels = new string[]{"id","name","hidden","desc","icon","leader","townhall","townlevel2","townlevel3","townlevel4","townLevelChangeable-food","townLevelChangeable-gold","terrain-forrest","#origin","ethos","researchElement","hometerrain","townNameGenerator","townLevelName1","townLevelName2","townLevelName3","townLevelName4"};
		private Nation[] _rows = new Nation[9];
		public void Init() {
			_rows = new Nation[]{
					new Nation("north","North",false,"Default nation",76,"nking","ntownhall","faith","safety","culture",0.2f,0.1f,0,"n","good","metal","grass","north","Village","Bigger village","Town","Bigger town"),
					new Nation("ranger","Rangers",false,"Nation under construction. Not really usefull at the moment.",584,"rnature","rpalast","","","",0.2f,-0.1f,-5,"magic","good","life","woods","elf","","","Kingdom","Bigger kingdom"),
					new Nation("fish","Fish people",true,"",0,"","","","","",0f,0f,0,"fight","neutral","water","ocean","underwater","","","",""),
					new Nation("dead","Unknown",true,"",298,"dnightking","","","","",0f,0f,0,"n","bad","death","underground","ghost","Cemetery","Bigger cemetery","",""),
					new Nation("forge","Forger",true,"",0,"","","","","",0f,0f,0,"magic","neutral","earth","mountain","dwarf","","","",""),
					new Nation("emis","Emissaries",true,"",0,"","","","","",0f,0f,0,"magic","bad","energy","hill","steam","","","",""),
					new Nation("harv","Harvester",true,"",0,"","","","","",0f,0f,0,"fight","good","wood","marsh","viking","","","",""),
					new Nation("mess","Messenger",true,"",0,"","","","","",0f,0f,0,"n","neutral","air","clouds","sky","","","",""),
					new Nation("fire","fire Worshipers",true,"",0,"ffireleader","","","","",0f,0f,0,"fight","bad","fire","lava","orc","","","","")
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetNation t;
			public SheetEnumerator(SheetNation t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		public int Length{ get{ return _rows.Length; } }
		public Nation this[int index]{
			get{
				return _rows[index];
			}
		}
		public Nation this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].id == id){ return _rows[i]; }
				}
				return null;
			}
		}
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].id == key){ return true; }
			}
			return false;
		}
		public Nation[] ToArray(){
			return _rows;
		}
		public Nation Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public Nation north{	get{ return _rows[0]; } }
		public Nation ranger{	get{ return _rows[1]; } }
		public Nation fish{	get{ return _rows[2]; } }
		public Nation dead{	get{ return _rows[3]; } }
		public Nation forge{	get{ return _rows[4]; } }
		public Nation emis{	get{ return _rows[5]; } }
		public Nation harv{	get{ return _rows[6]; } }
		public Nation mess{	get{ return _rows[7]; } }
		public Nation fire{	get{ return _rows[8]; } }

	}
}
namespace DataTypes{
	public partial class Building{
		public string id;
		public string name;
		public string file;
		public int width;
		public int height;
		public int reqbuildtownlevel;
		public string reqbuildnation;
		public string reqbuild1;
		public string reqbuild2;
		public string reqbuild3;
		public string cat;
		public int atk;
		public int def;
		public string connected;
		public int hp;
		public int ap;
		public int visible;
		public int buildtime;
		public string cost1;
		public string cost2;
		public string cost3;
		public string cost4;
		public string cost5;
		public string cost6;
		public string reqproduce1;
		public string reqproduce2;
		public string reqproduce3;
		public string reqproduce4;
		public string reqproduce5;
		public string produce1;
		public string produce2;
		public string produce3;
		public string produce4;
		public string produce5;
		public string action1;
		public string action2;
		public string action3;
		public string action4;
		public string action5;
		public string produceonce1;
		public string produceonce2;
		public string produceonce3;
		public string produceonce4;
		public string produceonce5;
		public string produceonce6;
		public int researchfire;
		public int researchwater;
		public int researchair;
		public int researchearth;
		public int researchmetal;
		public int researchwood;
		public int researchlife;
		public int researchdeath;
		public int researchenergy;
		public string actionalchemyP;
		public int actiondestroyTown;
		public int actionsendRess;
		public string reqscenconf;
		public string reqterrain;
		public string reqmax;
		public string reqmin;
		public int passiveActionupgradeRangeLimit;
		public int passiveActionupgradeBuildingLimit;
		public int passiveActionstorage;
		public int passiveActionupgradeTownLimit;
		public int passiveActionupgradeTownLevel;
		public string passiveActionenablePlayerFeature;
		public int passPublicwalk;
		public int passOwnerwalk;
		public int passOwnerswim;

		public Building(){}

		public Building(string id, string name, string file, int width, int height, int reqbuildtownlevel, string reqbuildnation, string reqbuild1, string reqbuild2, string reqbuild3, string cat, int atk, int def, string connected, int hp, int ap, int visible, int buildtime, string cost1, string cost2, string cost3, string cost4, string cost5, string cost6, string reqproduce1, string reqproduce2, string reqproduce3, string reqproduce4, string reqproduce5, string produce1, string produce2, string produce3, string produce4, string produce5, string action1, string action2, string action3, string action4, string action5, string produceonce1, string produceonce2, string produceonce3, string produceonce4, string produceonce5, string produceonce6, int researchfire, int researchwater, int researchair, int researchearth, int researchmetal, int researchwood, int researchlife, int researchdeath, int researchenergy, string actionalchemyP, int actiondestroyTown, int actionsendRess, string reqscenconf, string reqterrain, string reqmax, string reqmin, int passiveActionupgradeRangeLimit, int passiveActionupgradeBuildingLimit, int passiveActionstorage, int passiveActionupgradeTownLimit, int passiveActionupgradeTownLevel, string passiveActionenablePlayerFeature, int passPublicwalk, int passOwnerwalk, int passOwnerswim){
			this.id = id;
			this.name = name;
			this.file = file;
			this.width = width;
			this.height = height;
			this.reqbuildtownlevel = reqbuildtownlevel;
			this.reqbuildnation = reqbuildnation;
			this.reqbuild1 = reqbuild1;
			this.reqbuild2 = reqbuild2;
			this.reqbuild3 = reqbuild3;
			this.cat = cat;
			this.atk = atk;
			this.def = def;
			this.connected = connected;
			this.hp = hp;
			this.ap = ap;
			this.visible = visible;
			this.buildtime = buildtime;
			this.cost1 = cost1;
			this.cost2 = cost2;
			this.cost3 = cost3;
			this.cost4 = cost4;
			this.cost5 = cost5;
			this.cost6 = cost6;
			this.reqproduce1 = reqproduce1;
			this.reqproduce2 = reqproduce2;
			this.reqproduce3 = reqproduce3;
			this.reqproduce4 = reqproduce4;
			this.reqproduce5 = reqproduce5;
			this.produce1 = produce1;
			this.produce2 = produce2;
			this.produce3 = produce3;
			this.produce4 = produce4;
			this.produce5 = produce5;
			this.action1 = action1;
			this.action2 = action2;
			this.action3 = action3;
			this.action4 = action4;
			this.action5 = action5;
			this.produceonce1 = produceonce1;
			this.produceonce2 = produceonce2;
			this.produceonce3 = produceonce3;
			this.produceonce4 = produceonce4;
			this.produceonce5 = produceonce5;
			this.produceonce6 = produceonce6;
			this.researchfire = researchfire;
			this.researchwater = researchwater;
			this.researchair = researchair;
			this.researchearth = researchearth;
			this.researchmetal = researchmetal;
			this.researchwood = researchwood;
			this.researchlife = researchlife;
			this.researchdeath = researchdeath;
			this.researchenergy = researchenergy;
			this.actionalchemyP = actionalchemyP;
			this.actiondestroyTown = actiondestroyTown;
			this.actionsendRess = actionsendRess;
			this.reqscenconf = reqscenconf;
			this.reqterrain = reqterrain;
			this.reqmax = reqmax;
			this.reqmin = reqmin;
			this.passiveActionupgradeRangeLimit = passiveActionupgradeRangeLimit;
			this.passiveActionupgradeBuildingLimit = passiveActionupgradeBuildingLimit;
			this.passiveActionstorage = passiveActionstorage;
			this.passiveActionupgradeTownLimit = passiveActionupgradeTownLimit;
			this.passiveActionupgradeTownLevel = passiveActionupgradeTownLevel;
			this.passiveActionenablePlayerFeature = passiveActionenablePlayerFeature;
			this.passPublicwalk = passPublicwalk;
			this.passOwnerwalk = passOwnerwalk;
			this.passOwnerswim = passOwnerswim;
		}
	}
	public class SheetBuilding: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,9,3,11,24,10);
		public readonly string[] labels = new string[]{"id","name","file","width","height","req-build-townlevel","req-build-nation","req-build1","req-build2","req-build3","cat","atk","def","connected","hp","ap","visible","buildtime","cost1","cost2","cost3","cost4","cost5","cost6","req-produce1","req-produce2","req-produce3","req-produce4","req-produce5","produce1","produce2","produce3","produce4","produce5","action1","action2","action3","action4","action5","produceonce1","produceonce2","produceonce3","produceonce4","produceonce5","produceonce6","research-fire","research-water","research-air","research-earth","research-metal","research-wood","research-life","research-death","research-energy","action-alchemyP","action-destroyTown","action-sendRess","req-scenconf","req-terrain","req-max","req-min","passiveAction-upgradeRangeLimit","passiveAction-upgradeBuildingLimit","passiveAction-storage","passiveAction-upgradeTownLimit","passiveAction-upgradeTownLevel","passiveAction-enablePlayerFeature","passPublic-walk","passOwner-walk","passOwner-swim"};
		private Building[] _rows = new Building[53];
		public void Init() {
			_rows = new Building[]{
					new Building("nlogger2","Bigger logger","north-way:nlogger2",1,1,2,"north","terrain:forest","field:building,nlogger","","prod",0,0,"",20,5,2,5,"wood:10","stone:4","","worker:2","tool:2","","","","","","","wood:6","","","","","","","","","","","","","","","",0,0,0,0,1,1,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nlogger","Logger","north-castle:nlogger",1,1,1,"north","terrain:forest","","","prod",0,0,"",5,5,1,2,"wood:2","stone:","","worker:1","","","","","","","","wood:2","","","","","destroy","buildUpgrade:nlogger2","","","","","","","","","",0,0,0,0,0,0,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nquarry2","Bigger quarry","north-castle:nquarry2",1,1,3,"north","terrain:hill,mountain","field:building,nquarry","","prod",0,0,"",30,5,2,7,"wood:9","stone:6","plank:2","worker:2","tool:2","","","","","","","stone:9","","","","","","","","","","","","","","","",1,0,0,1,0,0,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nquarry","Quarry","north-castle:nquarry",1,1,1,"north","terrain:hill,mountain","","","prod",0,0,"",10,5,1,2,"wood:3","","","worker:1","tool:1","","","","","","","stone:3","","","","","destroy","buildUpgrade:nquarry2","","","","","","","","","",0,0,0,0,0,0,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nhunter","Hunter","north-farm:nhunter",1,1,1,"north","terrain:forest,grass","","","food",0,0,"",5,5,1,3,"wood:4","stone:1","","worker:1","tool:1","","","","","","","food:3","","","","","destroy","","","","","","","","","","",0,0,0,0,0,0,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nfisher","Fisher","nfisher",1,1,1,"north","terrainNear:water","","","food",0,0,"",5,5,1,4,"wood:2","stone:2","","worker:1","tool:1","","","","","","","food:4","","","","","destroy","","","","","","","","","","",0,0,0,0,0,0,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("ntownhall4","Bigger town hall","north-way:ntownhall4",2,1,3,"north","field:building,ntownhall3","","","needs",0,0,"",100,5,5,10,"","","plank:8","brick:15","","","","","","","","gold:1","","","","","","train:nworker,nexplorer,nsettler","","","","culture:10","","","","","",0,0,1,0,0,0,1,0,1,"",1,1,"","","","",5,70,300,2,1,"",0,0,0),
					new Building("ntownhall3","Town hall","north-way:ntownhall3",1,1,2,"north","field:building,ntownhall2","","","needs",0,0,"",50,5,4,7,"wood:4","stone:24","plank:4","","","","","","","","","gold:1","","","","","","train:nworker,nexplorer,nsettler","buildUpgrade:ntownhall4","","","","","","","","",0,0,0,0,0,0,1,0,1,"",1,1,"","","","",5,45,200,1,1,"",0,0,0),
					new Building("ntownhall2","Bigger village hall","north-tower:ntownhall2",1,1,1,"north","field:building,ntownhall","","","needs",0,0,"",25,5,3,4,"wood:4","stone:6","","","","","","","","","","gold:1","","","","","","train:nworker,nexplorer","buildUpgrade:ntownhall3","","","","","","","","",0,0,0,0,0,0,1,0,0,"",1,0,"","","","",5,25,200,0,1,"",0,0,0),
					new Building("ntownhall","Village hall","north-tower:ntownhall",1,1,1,"north","","","","needs",0,0,"",10,5,2,1,"","","","","","","","","","","","gold:1","","","","","","train:nworker,nexplorer","buildUpgrade:ntownhall2","trade:buy,tools","","food:20","stone:8","wood:15","tool:10","worker:6","workermax:6",0,0,0,0,0,0,0,0,0,"",1,0,"","","","",0,10,300,0,0,"",0,0,0),
					new Building("nsawmill","Sawmill","north-more:nsawmill",1,1,2,"north","terrainNear:water","","","prod",0,0,"",20,5,1,5,"wood:20","stone:10","","worker:1","tool:3","","daytime:not-night","ressMin:wood,2","","","","wood:-2","plank:1","","","","destroy","","","","","","","","","","",0,0,0,0,1,1,0,0,1,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nmarket","Market","north-tower:nmarket",1,1,2,"north","townNear","","","needs",0,0,"",20,5,1,7,"wood:24","stone:6","","worker:1","gold:3","","","","","","","gold:1","","","","","destroy","","trade","","","culture:10","","","","","",0,0,1,0,1,0,1,0,0,"",0,1,"","","2-nmarket-false","",0,0,0,0,0,"",0,0,0),
					new Building("nmine","Mine","889",0,0,2,"north","terrain:mountain","","","prod",0,0,"",20,5,0,7,"wood:12","stone:8","plank:3","worker:1","gold:1","","","","","","","ore:1","","","","","destroy","","","","","","","","","","",0,0,0,1,1,0,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nworkshop","Workshop","1176",0,0,2,"north","townNear","","","prod",0,0,"",20,5,1,5,"wood:8","stone:20","plank:4","worker:2","","","daytime:not-night","ressMin:wood,1","ressMin:ore,1","","","wood:-1","ore:-1","tool:1","","","destroy","","","","","","","","","","",1,0,1,0,1,1,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nfarm2","Bigger farm","north-farm:nfarm2",2,1,3,"north","terrain:grass","field:building,nfarm","","food",0,0,"",30,5,1,7,"wood:12","stone:9","plank:5","worker:2","tool:3","","","","","","","","","","","","destroy","","","","","","","","","","",0,1,1,0,0,0,1,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nfarm","Farm","north-farm:nfarm",1,1,2,"north","terrain:grass","","","food",0,0,"",20,5,1,5,"wood:12","stone:8","","worker:1","tool:2","","season:not-winter","","","","","food:5","","","","","destroy","buildUpgrade:nfarm2","","","","","","","","","",0,1,0,0,0,0,1,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nfield","Field","north-farm:nfield",1,1,2,"north","terrain:grass","","","food",0,0,"",1,5,1,2,"","","plank:1","","","","season:not-winter","","","","","food:3","","","","","destroy","","","","","","","","","","",0,1,0,1,0,0,0,0,0,"",0,0,"","","","1-nfarm2-false",0,0,0,0,0,"",0,0,0),
					new Building("nwall","Town wall","1906",0,0,3,"north","","","","war",0,0,"wall",100,5,1,4,"","stone:3","","brick:3","","","","","","","","","","","","","destroy","","","","","culture:10","","","","","safety:3",0,0,0,0,1,0,0,1,1,"",0,0,"","","","",0,1,0,0,0,"",0,0,0),
					new Building("ndockyard","Dockyard","addon:dock",1,1,3,"north","terrainNear:water","","","explo",0,0,"",30,5,1,7,"worker:2","stone:6","plank:3","brick:1","tool:2","","","","","","","","","","","","destroy","train:nship","trade","","","","","","","","safety:10",0,1,1,0,0,1,0,0,0,"",0,1,"","","","",0,0,0,0,0,"",0,0,5),
					new Building("nstreet2","Street","1910",0,0,3,"north","townNear","field:building,nstreet","","explo",0,0,"wall",30,5,1,7,"","stone:5","","brick:1","gold:2","","","","","","","","","","","","","","","","","","","","","","",0,0,1,0,1,0,1,0,0,"",0,0,"","","","",0,1,0,0,0,"",-2,2,0),
					new Building("nstreet","Way","1654",0,0,2,"north","townNear","","","explo",0,0,"wall",20,5,1,4,"wood:3","","","","gold:1","","","","","","","","","","","","destroy","buildUpgrade:nstreet2","","","","","","","","","",0,0,0,0,1,0,1,0,0,"",0,0,"","","","",0,1,0,0,0,"",-2,-2,0),
					new Building("nbrickfactory","Brick factory","788",0,0,3,"north","townNear","","","prod",0,0,"",30,5,1,7,"worker:2","stone:36","plank:4","","tool:3","","daytime:not-night","ressMin:stone,2","","","","stone:-2","brick:1","","","","destroy","","","","","","","","","","",1,0,0,1,0,1,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("narmoury","Armoury","1210",0,0,3,"north","townNear","","","war",0,0,"",30,5,1,7,"worker:3","stone:13","plank:9","brick:7","tool:4","","daytime:not-night","ressMin:wood,3","ressMin:tool,1","ressMin:ore,2","","wood:-3","tool:-1","ore:-2","weapon:1","","destroy","","","","","","","","","","",1,0,0,1,1,0,0,1,1,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nbarracks","Barracks","north-more:nbaracks",1,1,3,"north","townNear","","","war",0,0,"",60,5,1,7,"worker:2","stone:7","plank:5","brick:9","tool:3","","","","","","","","","","","","destroy","train","","","","","","","","","safety:20",1,0,0,0,1,0,0,1,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("ncastle","Castle","north-tower:ncastle",2,2,4,"north","townNear","","","war",5,1,"",100,5,1,9,"worker:3","stone:14","plank:5","brick:12","tool:6","","","","","","","","","","","","destroy","","","","","","","","","","safety:30",1,0,0,0,1,0,0,1,1,"",0,0,"","","0-ncastle-true","",5,5,0,1,2,"",0,0,0),
					new Building("npalace","Palace","north-more:nstorage",2,2,4,"north","townNear","","","deco",0,0,"",40,5,1,9,"worker:3","gold:10","plank:10","brick:10","tool:10","","","","","","","research:2","","","","","destroy","","","","","culture:40","","","","faith:10","",0,0,0,0,1,0,1,1,1,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nstorage","Storage","north-more:nstorage",1,1,2,"north","townNear","","","general",0,0,"",20,5,1,5,"wood:24","stone:10","","","worker:1","","","","","","","","","","","","destroy","","","","","","","","","","",0,0,1,0,0,1,0,0,0,"",0,0,"","","","",0,0,300,0,0,"",0,0,0),
					new Building("nlibrary","Library","999",0,0,1,"north","townNear","","","general",0,0,"",10,5,1,4,"wood:1","stone:4","","","worker:1","","","","","","","research:1","","","","","destroy","","","","","","","","","","",0,0,0,0,0,0,0,0,0,"",0,0,"RESEARCH","","","",0,0,0,0,0,"RESEARCH",0,0,0),
					new Building("nresearch","Research tower","north-tower:nrtower",1,2,3,"north","townNear","","","general",0,0,"",30,5,1,7,"wood:9","stone:6","","brick:6","worker:2","","","ressMin:gold,3","","","","research:3","gold:-1","","","","destroy","","","","","","","","","","",1,1,1,1,0,0,0,0,0,"",0,0,"RESEARCH","","","",0,0,0,0,0,"RESEARCH",0,0,0),
					new Building("nbridge","Bridge","851",0,0,3,"north","","","","explo",0,0,"bridge",30,5,1,5,"","stone:6","plank:1","brick:4","tool:2","","","","","","","","","","","","destroy","","","","","","","","","","",0,1,0,1,0,0,0,0,0,"",0,0,"","river","","",0,0,0,0,0,"",0,5,0),
					new Building("ntemple2","Bigger circle","addon:ncircle2",1,1,4,"north","townNear","field:building,ntemple","","needs",0,0,"",40,5,1,7,"worker:2","","plank:4","brick:10","tool:3","","","","","","","","","","","","destroy","","","","","culture:10","","","","faith:30","",0,0,0,0,0,0,1,1,1,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("ntemple","Circle","addon:ncircle",1,1,2,"north","townNear","","","needs",0,0,"",20,5,1,3,"wood:4","stone:11","worker:1","","tool:1","","","","","","","","","","","","destroy","buildUpgrade:ntemple2","","","","culture:5","","","","faith:15","",1,1,0,0,0,0,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("ncircus","Circus","addon:ncircus",1,1,4,"north","townNear","","","needs",0,0,"",40,5,1,5,"wood:16","worker:2","plank:8","brick:2","tool:4","","","","","","","food:-1","","","","","destroy","","","","","culture:25","","","","","",0,0,0,1,0,1,1,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nwoodtower","Wood tower","north-tower:nwoodtower",1,1,2,"north","","","","explo",1,0,"",20,5,5,3,"wood:12","","plank:1","","","","","","","","","","","","","","destroy","","","","","","","","","","",0,0,1,0,0,1,1,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nhouse2","House","north-more:nhouse",1,1,2,"north","townNear","field:building,nhouse","","needs",0,0,"",20,5,1,7,"wood:4","stone:15","plank:3","","tool:2","","","","","","","","","","","","destroy","","","","","worker:15","","","","","",0,0,0,0,0,1,1,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nhouse","Tent","199",0,0,1,"north","townNear","","","needs",0,0,"",2,5,1,2,"wood:1","","","","","","","","","","","","","","","","destroy","buildUpgrade:nhouse2","","","","worker:6","","","","","",0,0,0,0,0,0,0,0,0,"",0,0,"","","5-nhouse-false","",0,0,0,0,0,"",0,0,0),
					new Building("rrope","Rope Factory","819",0,0,2,"ranger","townNear","","","prod",0,0,"",0,5,1,5,"wood:4","","leaf:4","","","worker:2","","ressMin:leaf,4","","","","leaf:-4","rope:1","","","","","","","","","","","","","","",0,0,0,0,0,1,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rleaf","Leaf collector","194",0,0,1,"ranger","terrain:forest","","","prod",0,0,"",0,5,1,2,"wood:3","food:8","","","","worker:1","season:not-winter","","","","","leaf:3","","","","","","","","","","","","","","","",0,0,0,0,0,0,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rsling","Slingshot workshop","1114",0,0,3,"ranger","townNear","","","prod",0,0,"",0,5,1,9,"wood:5","","leaf:6","plank:4","rope:4","worker:2","","ressMin:plank,1","ressMin:rope,2","","","plank:-1","rope:-2","slingshot:1","","","","","","","","","","","","","",0,0,1,0,0,1,0,1,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rplank","Plank creater","743",0,0,2,"ranger","townNear","","","prod",0,0,"",0,5,1,6,"wood:11","","leaf:7","","rope:2","worker:1","","ressMin:wood,2","","","","wood:-2","plank:1","","","","","","","","","","","","","","",0,0,0,0,0,1,0,0,1,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rwood","Wood collector","1838",0,0,1,"ranger","terrain:forest","","","prod",0,0,"",0,5,1,3,"","food:4","","","","worker:1","season:not-spring","","","","","wood:3","","","","","","","","","","","","","","","",0,0,0,0,0,0,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rberry","Berry finder","132",0,0,1,"ranger","terrain:grass","","","food",0,0,"",0,5,1,2,"wood:4","","leaf:3","","","worker:1","season:not-winter","","","","","food:2","","","","","","","","","","","","","","","",0,0,0,0,0,0,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rwapple","Winter apple","958",0,0,2,"ranger","terrain:grass","","","food",0,0,"",0,5,1,3,"wood:1","","leaf:4","","rope:1","","season:winter","","","","","food:6","","","","","","","","","","","","","","","",0,0,0,0,0,1,1,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rhouse","Tree house","1316",0,0,1,"ranger","terrain:forest","","","needs",0,0,"",0,5,1,3,"wood:5","","leaf:7","","","","","","","","","","","","","","","","","","","worker:6","","","","","",0,0,0,0,0,0,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rresearch","Mediation","1900",0,0,1,"ranger","townNear","","","general",0,0,"",0,5,1,5,"wood:5","","","","gold:2","worker:1","","","","","","research:1","","","","","","","","","","","","","","","",0,0,0,0,0,0,0,0,0,"",0,0,"RESEARCH","","","",0,0,0,0,0,"RESEARCH",0,0,0),
					new Building("rtrain","Training","1871",0,0,3,"ranger","townNear","","","war",0,0,"",0,5,1,7,"wood:2","slingshot:1","leaf:5","plank:3","rope:3","worker:2","","","","","","","","","","","train","","","","","","","","","","",0,0,0,0,0,0,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rpalast5","Biggest palast","66",0,0,4,"ranger","","field:building,rpalast4","","general",0,0,"",0,5,10,28,"","","","","","","","","","","","worker:1","","","","","train:rworker,rexplorer","","trade","","","","","","","","",0,0,0,0,0,0,0,0,0,"",1,0,"","","","",40,40,400,0,1,"",0,0,0),
					new Building("rpalast4","Bigger palast","34",0,0,3,"ranger","","field:building,rpalast3","","general",0,0,"",0,5,8,10,"","","","","","","","","","","","worker:1","","","","","train:rworker,rexplorer","buildUpgrade:rpalast5","trade","","","","","","","","",0,0,0,0,0,0,0,0,0,"",1,0,"","","","",20,40,300,0,1,"",0,0,0),
					new Building("rpalast3","Big palast","34",0,0,2,"ranger","","field:building,rpalast2","","general",0,0,"",0,5,6,4,"wood:4","","","","","","","","","","","worker:1","","","","","train:rworker,rexplorer","buildUpgrade:rpalast4","trade","","","","","","","","",0,0,0,0,0,0,0,0,0,"",1,0,"","","","",20,30,200,0,1,"",0,0,0),
					new Building("rpalast2","Medium palast","959",0,0,1,"ranger","","field:building,rpalast1","","general",0,0,"",0,5,4,2,"wood:4","","leaf:4","","","","","","","","","worker:1","","","","","train:rworker,rexplorer","buildUpgrade:rpalast3","trade","","","","","","","","",0,0,0,0,0,0,0,0,0,"",1,0,"","","","",10,20,200,0,1,"",0,0,0),
					new Building("rpalast","Small palast","959",0,0,1,"ranger","","","","general",0,0,"",0,5,2,1,"","","","","","","","","","","","worker:1","","","","","train:rworker,rexplorer","buildUpgrade:rpalast2","trade","","","worker:5","food:20","wood:10","leaf:5","","",0,0,0,0,0,0,0,0,0,"",1,0,"","","","",10,20,300,0,0,"",0,0,0),
					new Building("ralchemy","Alchemy","1462",0,0,4,"ranger","townNear","","","prod",0,0,"",0,5,1,5,"wood:6","leaf:15","plank:6","rope:4","gold:2","worker:2","","","","","","","","","","","","","","","","","","","","","",1,0,0,0,1,0,0,1,1,"plank-4",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rstreet","Way","1654",0,0,2,"ranger","townNear","","","explo",0,0,"",0,5,0,5,"wood:1","","leaf:4","","","","","","","","","","","","","","","","","","","","","","","","",0,0,0,1,0,1,0,0,0,"",0,0,"","","","",0,0,0,0,0,"",0,0,0)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetBuilding t;
			public SheetEnumerator(SheetBuilding t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		public int Length{ get{ return _rows.Length; } }
		public Building this[int index]{
			get{
				return _rows[index];
			}
		}
		public Building this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].id == id){ return _rows[i]; }
				}
				return null;
			}
		}
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].id == key){ return true; }
			}
			return false;
		}
		public Building[] ToArray(){
			return _rows;
		}
		public Building Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public Building nlogger2{	get{ return _rows[0]; } }
		public Building nlogger{	get{ return _rows[1]; } }
		public Building nquarry2{	get{ return _rows[2]; } }
		public Building nquarry{	get{ return _rows[3]; } }
		public Building nhunter{	get{ return _rows[4]; } }
		public Building nfisher{	get{ return _rows[5]; } }
		public Building ntownhall4{	get{ return _rows[6]; } }
		public Building ntownhall3{	get{ return _rows[7]; } }
		public Building ntownhall2{	get{ return _rows[8]; } }
		public Building ntownhall{	get{ return _rows[9]; } }
		public Building nsawmill{	get{ return _rows[10]; } }
		public Building nmarket{	get{ return _rows[11]; } }
		public Building nmine{	get{ return _rows[12]; } }
		public Building nworkshop{	get{ return _rows[13]; } }
		public Building nfarm2{	get{ return _rows[14]; } }
		public Building nfarm{	get{ return _rows[15]; } }
		public Building nfield{	get{ return _rows[16]; } }
		public Building nwall{	get{ return _rows[17]; } }
		public Building ndockyard{	get{ return _rows[18]; } }
		public Building nstreet2{	get{ return _rows[19]; } }
		public Building nstreet{	get{ return _rows[20]; } }
		public Building nbrickfactory{	get{ return _rows[21]; } }
		public Building narmoury{	get{ return _rows[22]; } }
		public Building nbarracks{	get{ return _rows[23]; } }
		public Building ncastle{	get{ return _rows[24]; } }
		public Building npalace{	get{ return _rows[25]; } }
		public Building nstorage{	get{ return _rows[26]; } }
		public Building nlibrary{	get{ return _rows[27]; } }
		public Building nresearch{	get{ return _rows[28]; } }
		public Building nbridge{	get{ return _rows[29]; } }
		public Building ntemple2{	get{ return _rows[30]; } }
		public Building ntemple{	get{ return _rows[31]; } }
		public Building ncircus{	get{ return _rows[32]; } }
		public Building nwoodtower{	get{ return _rows[33]; } }
		public Building nhouse2{	get{ return _rows[34]; } }
		public Building nhouse{	get{ return _rows[35]; } }
		public Building rrope{	get{ return _rows[36]; } }
		public Building rleaf{	get{ return _rows[37]; } }
		public Building rsling{	get{ return _rows[38]; } }
		public Building rplank{	get{ return _rows[39]; } }
		public Building rwood{	get{ return _rows[40]; } }
		public Building rberry{	get{ return _rows[41]; } }
		public Building rwapple{	get{ return _rows[42]; } }
		public Building rhouse{	get{ return _rows[43]; } }
		public Building rresearch{	get{ return _rows[44]; } }
		public Building rtrain{	get{ return _rows[45]; } }
		public Building rpalast5{	get{ return _rows[46]; } }
		public Building rpalast4{	get{ return _rows[47]; } }
		public Building rpalast3{	get{ return _rows[48]; } }
		public Building rpalast2{	get{ return _rows[49]; } }
		public Building rpalast{	get{ return _rows[50]; } }
		public Building ralchemy{	get{ return _rows[51]; } }
		public Building rstreet{	get{ return _rows[52]; } }

	}
}
namespace DataTypes{
	public partial class Help{
		public string id;
		public string name;
		public string icon;
		public string text;

		public Help(){}

		public Help(string id, string name, string icon, string text){
			this.id = id;
			this.name = name;
			this.icon = icon;
			this.text = text;
		}
	}
	public class SheetHelp: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,9,3,11,24,11);
		public readonly string[] labels = new string[]{"id","name","icon","text"};
		private Help[] _rows = new Help[6];
		public void Init() {
			_rows = new Help[]{
					new Help("moveUnit","Move the units","base:move","At the moment, you can only move the units with the arrows, if they have enough AP. Every Round the AP will be refreshed."),
					new Help("moveCamera","Move the camera","base:map","To move the camera, please use WASD on the keyboard."),
					new Help("general","General","logo32","Hello! \nNice to meet you, i'm Sven and develop 9 Nations. It is a tuned-based strategy game. You have to explore the area and develop your kingdoms.\n"),
					new Help("beta","Warning","ui:debug","Hello,\nThis version of the game does not contains all features at the moment. This means that you can encounter bugs, crashes, incomplete and unpolished features, etc. Also except balancing issuses.\nPlease also \nIf you have found any mistakes, bugs oder you have questions, please use the feedback button. I'm happy to hear from you. \nGreetings Sven"),
					new Help("privacy","Privacy Policy","magic:privacy","Please take a look at the website https://9nations.de/privacy-policy/privacy-policy-in-game/"),
					new Help("new","Whats new?","base:new","")
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetHelp t;
			public SheetEnumerator(SheetHelp t){
				this.t = t;
			}
			public bool MoveNext(){
				if (idx < t._rows.Length - 1){
					idx++;
					return true;
				}else{
					return false;
				}
			}
			public void Reset(){
				idx = -1;
			}
			public object Current{
				get{
					return t._rows[idx];
				}
			}
		}
		public int Length{ get{ return _rows.Length; } }
		public Help this[int index]{
			get{
				return _rows[index];
			}
		}
		public Help this[string id]{
			get{
				for (int i = 0; i < _rows.Length; i++) {
					if( _rows[i].id == id){ return _rows[i]; }
				}
				return null;
			}
		}
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].id == key){ return true; }
			}
			return false;
		}
		public Help[] ToArray(){
			return _rows;
		}
		public Help Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public Help moveUnit{	get{ return _rows[0]; } }
		public Help moveCamera{	get{ return _rows[1]; } }
		public Help general{	get{ return _rows[2]; } }
		public Help beta{	get{ return _rows[3]; } }
		public Help privacy{	get{ return _rows[4]; } }
		public Help _new{	get{ return _rows[5]; } }

	}
}