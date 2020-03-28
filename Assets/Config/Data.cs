
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Data{
	//Document URL: https://spreadsheets.google.com/feeds/worksheets/1U663Qd5fqg1MNDhFQ6uvzYjlmshHd7Yyk8YIkib1CbQ/public/basic?alt=json-in-script

	//Sheet SheetRess
	public static DataTypes.SheetRess ress = new DataTypes.SheetRess();
	//Sheet SheetFeatures
	public static DataTypes.SheetFeatures features = new DataTypes.SheetFeatures();
	//Sheet SheetFeaturePlayer
	public static DataTypes.SheetFeaturePlayer featurePlayer = new DataTypes.SheetFeaturePlayer();
	//Sheet SheetInputKey
	public static DataTypes.SheetInputKey inputKey = new DataTypes.SheetInputKey();
	//Sheet SheetNAction
	public static DataTypes.SheetNAction nAction = new DataTypes.SheetNAction();
	//Sheet SheetMapAction
	public static DataTypes.SheetMapAction mapAction = new DataTypes.SheetMapAction();
	//Sheet SheetIcons
	public static DataTypes.SheetIcons icons = new DataTypes.SheetIcons();
	//Sheet SheetHelp
	public static DataTypes.SheetHelp help = new DataTypes.SheetHelp();
	static Data(){
		ress.Init(); features.Init(); featurePlayer.Init(); inputKey.Init(); nAction.Init(); mapAction.Init(); icons.Init(); help.Init(); 
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
		public System.DateTime updated = new System.DateTime(2020,3,28,14,4,25);
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
					new Ress("research","Research","","magic:research",0,0f,50),
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
	public partial class Features{
		public string id;
		public string name;
		public string icon;
		public string scope;
		public string standard;
		public string type;

		public Features(){}

		public Features(string id, string name, string icon, string scope, string standard, string type){
			this.id = id;
			this.name = name;
			this.icon = icon;
			this.scope = scope;
			this.standard = standard;
			this.type = type;
		}
	}
	public class SheetFeatures: IEnumerable{
		public System.DateTime updated = new System.DateTime(2020,3,28,14,4,26);
		public readonly string[] labels = new string[]{"id","name","icon","scope","standard","type"};
		private Features[] _rows = new Features[9];
		public void Init() {
			_rows = new Features[]{
					new Features("research","Enable Research","","game","TRUE","bool"),
					new Features("debug","Debug","","option","FALSE","bool"),
					new Features("fog","Fog of war","","game","TRUE","bool"),
					new Features("centermouse","Center map on mouse click","","option","FALSE","bool"),
					new Features("xxx","","","xxx","xxxx","bool"),
					new Features("uiScale","Scale the Ui","","xxx","1","scale,0.5,3"),
					new Features("autosave","Save the game automatical","","option","TRUE","bool"),
					new Features("update","Check for updates","","option","TRUE","bool"),
					new Features("showAction","Show every action","","option","FALSE","bool")
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetFeatures t;
			public SheetEnumerator(SheetFeatures t){
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
		public Features this[int index]{
			get{
				return _rows[index];
			}
		}
		public Features this[string id]{
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
		public Features[] ToArray(){
			return _rows;
		}
		public Features Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public Features research{	get{ return _rows[0]; } }
		public Features debug{	get{ return _rows[1]; } }
		public Features fog{	get{ return _rows[2]; } }
		public Features centermouse{	get{ return _rows[3]; } }
		public Features xxx{	get{ return _rows[4]; } }
		public Features uiScale{	get{ return _rows[5]; } }
		public Features autosave{	get{ return _rows[6]; } }
		public Features update{	get{ return _rows[7]; } }
		public Features showAction{	get{ return _rows[8]; } }

	}
}
namespace DataTypes{
	public partial class FeaturePlayer{
		public string id;
		public string name;
		public string icon;
		public string scope;
		public string standard;
		public string type;

		public FeaturePlayer(){}

		public FeaturePlayer(string id, string name, string icon, string scope, string standard, string type){
			this.id = id;
			this.name = name;
			this.icon = icon;
			this.scope = scope;
			this.standard = standard;
			this.type = type;
		}
	}
	public class SheetFeaturePlayer: IEnumerable{
		public System.DateTime updated = new System.DateTime(2020,3,28,14,4,27);
		public readonly string[] labels = new string[]{"id","name","icon","scope","standard","type"};
		private FeaturePlayer[] _rows = new FeaturePlayer[2];
		public void Init() {
			_rows = new FeaturePlayer[]{
					new FeaturePlayer("research","Research","","player","FALSE","bool"),
					new FeaturePlayer("xxx","","","","xxxx","bool")
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetFeaturePlayer t;
			public SheetEnumerator(SheetFeaturePlayer t){
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
		public FeaturePlayer this[int index]{
			get{
				return _rows[index];
			}
		}
		public FeaturePlayer this[string id]{
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
		public FeaturePlayer[] ToArray(){
			return _rows;
		}
		public FeaturePlayer Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public FeaturePlayer research{	get{ return _rows[0]; } }
		public FeaturePlayer xxx{	get{ return _rows[1]; } }

	}
}
namespace DataTypes{
	public partial class InputKey{
		public string id;
		public string key;
		public string type;
		public bool hidden;
		public bool active;

		public InputKey(){}

		public InputKey(string id, string key, string type, bool hidden, bool active){
			this.id = id;
			this.key = key;
			this.type = type;
			this.hidden = hidden;
			this.active = active;
		}
	}
	public class SheetInputKey: IEnumerable{
		public System.DateTime updated = new System.DateTime(2020,3,28,14,4,27);
		public readonly string[] labels = new string[]{"id","key","type","hidden","active"};
		private InputKey[] _rows = new InputKey[33];
		public void Init() {
			_rows = new InputKey[]{
					new InputKey("research","F1","gameButton",false,false),
					new InputKey("lexicon","F2","gameButton",false,false),
					new InputKey("quest","F3","gameButton",false,false),
					new InputKey("debug","F4","gameButton",false,false),
					new InputKey("nextround","Space","gameButton",false,false),
					new InputKey("options","O","gameButton",false,false),
					new InputKey("save","F12","gameButton",false,false),
					new InputKey("load","F11","gameButton",false,false),
					new InputKey("kingdom","F5","gameButton",false,false),
					new InputKey("mainmenu","Escape","gameButton",false,false),
					new InputKey("nextUnit",",","gameButton",false,false),
					new InputKey("destroy","K","action",false,true),
					new InputKey("build","B","action",false,true),
					new InputKey("foundTown","Y","action",false,true),
					new InputKey("train","N","action",false,true),
					new InputKey("buildUpgrade","V","action",false,true),
					new InputKey("trainUpgrade","C","action",false,true),
					new InputKey("trade","T","action",false,true),
					new InputKey("sleep","Z","action",false,true),
					new InputKey("moveUnitEast","LeftArrow","hidden",false,true),
					new InputKey("moveUnitSouth","DownArrow","hidden",false,true),
					new InputKey("moveUnitWest","RightArrow","hidden",false,true),
					new InputKey("moveUnitNorth","UpArrow","hidden",false,true),
					new InputKey("moveCameraEast","A","hidden",false,false),
					new InputKey("moveCameraSouth","S","hidden",false,false),
					new InputKey("moveCameraWest","D","hidden",false,false),
					new InputKey("moveCameraNorth","W","hidden",false,false),
					new InputKey("zoomCameraIn","E","hidden",false,false),
					new InputKey("zoomCameraOut","Q","hidden",false,false),
					new InputKey("moveLevelTop","R","hidden",false,false),
					new InputKey("moveLevelDown","F","hidden",false,false),
					new InputKey("minimap","M","hidden",true,false),
					new InputKey("go","G","action",false,true)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetInputKey t;
			public SheetEnumerator(SheetInputKey t){
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
		public InputKey this[int index]{
			get{
				return _rows[index];
			}
		}
		public InputKey this[string id]{
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
		public InputKey[] ToArray(){
			return _rows;
		}
		public InputKey Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public InputKey research{	get{ return _rows[0]; } }
		public InputKey lexicon{	get{ return _rows[1]; } }
		public InputKey quest{	get{ return _rows[2]; } }
		public InputKey debug{	get{ return _rows[3]; } }
		public InputKey nextround{	get{ return _rows[4]; } }
		public InputKey options{	get{ return _rows[5]; } }
		public InputKey save{	get{ return _rows[6]; } }
		public InputKey load{	get{ return _rows[7]; } }
		public InputKey kingdom{	get{ return _rows[8]; } }
		public InputKey mainmenu{	get{ return _rows[9]; } }
		public InputKey nextUnit{	get{ return _rows[10]; } }
		public InputKey destroy{	get{ return _rows[11]; } }
		public InputKey build{	get{ return _rows[12]; } }
		public InputKey foundTown{	get{ return _rows[13]; } }
		public InputKey train{	get{ return _rows[14]; } }
		public InputKey buildUpgrade{	get{ return _rows[15]; } }
		public InputKey trainUpgrade{	get{ return _rows[16]; } }
		public InputKey trade{	get{ return _rows[17]; } }
		public InputKey sleep{	get{ return _rows[18]; } }
		public InputKey moveUnitEast{	get{ return _rows[19]; } }
		public InputKey moveUnitSouth{	get{ return _rows[20]; } }
		public InputKey moveUnitWest{	get{ return _rows[21]; } }
		public InputKey moveUnitNorth{	get{ return _rows[22]; } }
		public InputKey moveCameraEast{	get{ return _rows[23]; } }
		public InputKey moveCameraSouth{	get{ return _rows[24]; } }
		public InputKey moveCameraWest{	get{ return _rows[25]; } }
		public InputKey moveCameraNorth{	get{ return _rows[26]; } }
		public InputKey zoomCameraIn{	get{ return _rows[27]; } }
		public InputKey zoomCameraOut{	get{ return _rows[28]; } }
		public InputKey moveLevelTop{	get{ return _rows[29]; } }
		public InputKey moveLevelDown{	get{ return _rows[30]; } }
		public InputKey minimap{	get{ return _rows[31]; } }
		public InputKey go{	get{ return _rows[32]; } }

	}
}
namespace DataTypes{
	public partial class NAction{
		public string id;
		public string name;
		public string desc;
		public int cost;
		public string icon;
		public bool activeMapElement;
		public string req1;
		public string req2;
		public bool useUnderConstruction;
		public bool onlyOwner;
		public string sound;
		public bool persistent;

		public NAction(){}

		public NAction(string id, string name, string desc, int cost, string icon, bool activeMapElement, string req1, string req2, bool useUnderConstruction, bool onlyOwner, string sound, bool persistent){
			this.id = id;
			this.name = name;
			this.desc = desc;
			this.cost = cost;
			this.icon = icon;
			this.activeMapElement = activeMapElement;
			this.req1 = req1;
			this.req2 = req2;
			this.useUnderConstruction = useUnderConstruction;
			this.onlyOwner = onlyOwner;
			this.sound = sound;
			this.persistent = persistent;
		}
	}
	public class SheetNAction: IEnumerable{
		public System.DateTime updated = new System.DateTime(2020,3,28,14,4,28);
		public readonly string[] labels = new string[]{"id","name","desc","cost","icon","activeMapElement","req1","req2","useUnderConstruction","onlyOwner","sound","persistent"};
		private NAction[] _rows = new NAction[19];
		public void Init() {
			_rows = new NAction[]{
					new NAction("destroy","Destroy it","Destroy it",0,"base:destroy",true,"","",true,true,"click",false),
					new NAction("build","Build","Construct a new Building",5,"base:build",true,"empty:building","townMin:1",false,true,"click",false),
					new NAction("foundTownFirst","Found your first town","Found your first town",10,"foundTown",true,"town:<0","",false,true,"click",false),
					new NAction("foundTown","Found a new town","Found a new town",10,"foundTown",true,"","",false,true,"click",false),
					new NAction("train","Train","Train a new unit",0,"base:train",true,"empty:unit","",false,true,"click",false),
					new NAction("buildUpgrade","Upgrade","Upgrade this building",0,"icons:upgrade",true,"","",false,true,"click",false),
					new NAction("trainUpgrade","Upgrade","Upgrade this unit",5,"icons:upgrade",true,"","",false,true,"click",false),
					new NAction("endGameLose","Lose","Lose the game",0,"base:destroy",true,"","",true,true,"click",false),
					new NAction("endGameWin","Win","Win the game",0,"other:win",true,"","",true,true,"click",false),
					new NAction("trade","Trade","Trade your ressources",2,"icons:trade",true,"townMin:1","",false,true,"click",false),
					new NAction("sleep","Sleep","Wait for the next round",1,"base:sleep",true,"","",false,true,"click",true),
					new NAction("featureP","Feature for player","Set the feature",0,"logo",false,"","",false,true,"click",false),
					new NAction("cameraMove","Move Camera","Move the camera",0,"logo",false,"","",false,true,"click",false),
					new NAction("gameButton","Call Game Button","Call a special game button",0,"logo",false,"","",false,true,"click",false),
					new NAction("move","Go","Move this unit",0,"base:move",true,"","",false,true,"click",false),
					new NAction("moveTo","Move to point","Move the unit to a specific point",0,"logo",true,"","",false,true,"click",true),
					new NAction("improvement","Convert to a map improvement\n","Convert to a map improvement\n",0,"logo",true,"","",false,true,"click",false),
					new NAction("townlevel","Develop town","Upgrade the town to a new level",0,"foundTown",true,"","",false,true,"click",false),
					new NAction("moveLevel","Move level","Move between the different level, like underground or overworld",15,"moveLevel",true,"","",false,true,"click",false)
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
		public NAction foundTownFirst{	get{ return _rows[2]; } }
		public NAction foundTown{	get{ return _rows[3]; } }
		public NAction train{	get{ return _rows[4]; } }
		public NAction buildUpgrade{	get{ return _rows[5]; } }
		public NAction trainUpgrade{	get{ return _rows[6]; } }
		public NAction endGameLose{	get{ return _rows[7]; } }
		public NAction endGameWin{	get{ return _rows[8]; } }
		public NAction trade{	get{ return _rows[9]; } }
		public NAction sleep{	get{ return _rows[10]; } }
		public NAction featureP{	get{ return _rows[11]; } }
		public NAction cameraMove{	get{ return _rows[12]; } }
		public NAction gameButton{	get{ return _rows[13]; } }
		public NAction move{	get{ return _rows[14]; } }
		public NAction moveTo{	get{ return _rows[15]; } }
		public NAction improvement{	get{ return _rows[16]; } }
		public NAction townlevel{	get{ return _rows[17]; } }
		public NAction moveLevel{	get{ return _rows[18]; } }

	}
}
namespace DataTypes{
	public partial class MapAction{
		public string id;
		public string name;
		public string desc;
		public int ap;
		public string icon;
		public string reqNonSelf1;
		public string reqNonSelf2;
		public string reqSelf1;
		public string reqSelf2;
		public string sound;

		public MapAction(){}

		public MapAction(string id, string name, string desc, int ap, string icon, string reqNonSelf1, string reqNonSelf2, string reqSelf1, string reqSelf2, string sound){
			this.id = id;
			this.name = name;
			this.desc = desc;
			this.ap = ap;
			this.icon = icon;
			this.reqNonSelf1 = reqNonSelf1;
			this.reqNonSelf2 = reqNonSelf2;
			this.reqSelf1 = reqSelf1;
			this.reqSelf2 = reqSelf2;
			this.sound = sound;
		}
	}
	public class SheetMapAction: IEnumerable{
		public System.DateTime updated = new System.DateTime(2020,3,28,14,4,28);
		public readonly string[] labels = new string[]{"id","name","desc","ap","icon","reqNonSelf1","reqNonSelf2","reqSelf1","reqSelf2","sound"};
		private MapAction[] _rows = new MapAction[3];
		public void Init() {
			_rows = new MapAction[]{
					new MapAction("attack","Attack","Fight the unit",10,"base:fight","unit:enemy","","hp:>25%","","attack"),
					new MapAction("leave","Leave","Leave the screen",0,"base:move","","","","","click"),
					new MapAction("heal","Heal","Heal the unit. The cure rate is based on the available AP.",2,"magic:heal","unit:own","hp:<99%","ap:>1","","click")
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetMapAction t;
			public SheetEnumerator(SheetMapAction t){
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
		public MapAction this[int index]{
			get{
				return _rows[index];
			}
		}
		public MapAction this[string id]{
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
		public MapAction[] ToArray(){
			return _rows;
		}
		public MapAction Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public MapAction attack{	get{ return _rows[0]; } }
		public MapAction leave{	get{ return _rows[1]; } }
		public MapAction heal{	get{ return _rows[2]; } }

	}
}
namespace DataTypes{
	public partial class Icons{
		public string id;
		public string file;

		public Icons(){}

		public Icons(string id, string file){
			this.id = id;
			this.file = file;
		}
	}
	public class SheetIcons: IEnumerable{
		public System.DateTime updated = new System.DateTime(2020,3,28,14,4,29);
		public readonly string[] labels = new string[]{"id","file"};
		private Icons[] _rows = new Icons[30];
		public void Init() {
			_rows = new Icons[]{
					new Icons("logo","Icons/logo"),
					new Icons("research","Icons/magic:research"),
					new Icons("quest","Icons/base:quest"),
					new Icons("move","Icons/base:move"),
					new Icons("foundTown","Icons/base:foundTown"),
					new Icons("help","Icons/magic:lexicon"),
					new Icons("debug","Icons/ui:debug"),
					new Icons("option","Icons/base:option"),
					new Icons("map","Icons/base:map"),
					new Icons("moveLevel","Icons/timeFantasy_ui:moveLevel"),
					new Icons("yes","Icons/ui:yes"),
					new Icons("no","Icons/ui:no"),
					new Icons("coat0","Icons/timeFantasy_fraction:coat0"),
					new Icons("coat1","Icons/timeFantasy_fraction:coat1"),
					new Icons("coat2","Icons/timeFantasy_fraction:coat2"),
					new Icons("coat3","Icons/timeFantasy_fraction:coat3"),
					new Icons("coat4","Icons/timeFantasy_fraction:coat4"),
					new Icons("coat5","Icons/timeFantasy_fraction:coat5"),
					new Icons("coat6","Icons/timeFantasy_fraction:coat6"),
					new Icons("coat7","Icons/timeFantasy_fraction:coat7"),
					new Icons("train","Icons/base:train"),
					new Icons("build","Icons/base:build"),
					new Icons("lexicon","Icons/magic:lexicon"),
					new Icons("res","Icons/base:res"),
					new Icons("ap","Icons/stats:ap"),
					new Icons("view","Icons/base:view"),
					new Icons("round","Icons/icons:nextRound"),
					new Icons("random","Icons/icons:random"),
					new Icons("hp","Icons/stats:hp"),
					new Icons("internet","Icons/magic:internet")
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetIcons t;
			public SheetEnumerator(SheetIcons t){
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
		public Icons this[int index]{
			get{
				return _rows[index];
			}
		}
		public Icons this[string id]{
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
		public Icons[] ToArray(){
			return _rows;
		}
		public Icons Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public Icons logo{	get{ return _rows[0]; } }
		public Icons research{	get{ return _rows[1]; } }
		public Icons quest{	get{ return _rows[2]; } }
		public Icons move{	get{ return _rows[3]; } }
		public Icons foundTown{	get{ return _rows[4]; } }
		public Icons help{	get{ return _rows[5]; } }
		public Icons debug{	get{ return _rows[6]; } }
		public Icons option{	get{ return _rows[7]; } }
		public Icons map{	get{ return _rows[8]; } }
		public Icons moveLevel{	get{ return _rows[9]; } }
		public Icons yes{	get{ return _rows[10]; } }
		public Icons no{	get{ return _rows[11]; } }
		public Icons coat0{	get{ return _rows[12]; } }
		public Icons coat1{	get{ return _rows[13]; } }
		public Icons coat2{	get{ return _rows[14]; } }
		public Icons coat3{	get{ return _rows[15]; } }
		public Icons coat4{	get{ return _rows[16]; } }
		public Icons coat5{	get{ return _rows[17]; } }
		public Icons coat6{	get{ return _rows[18]; } }
		public Icons coat7{	get{ return _rows[19]; } }
		public Icons train{	get{ return _rows[20]; } }
		public Icons build{	get{ return _rows[21]; } }
		public Icons lexicon{	get{ return _rows[22]; } }
		public Icons res{	get{ return _rows[23]; } }
		public Icons ap{	get{ return _rows[24]; } }
		public Icons view{	get{ return _rows[25]; } }
		public Icons round{	get{ return _rows[26]; } }
		public Icons random{	get{ return _rows[27]; } }
		public Icons hp{	get{ return _rows[28]; } }
		public Icons internet{	get{ return _rows[29]; } }

	}
}
namespace DataTypes{
	public partial class Help{
		public string unitsAreBesidesBuildingsEssentialToTheGameWhenClickedTheyCanBeMovedWithTheArrowKeysWhichCostsActionPointsAPYouCanAlsoPerformVariousActionsWhichCostsAPAtTheEndOfEachRoundTheAPWillBeRefilled;
		public string name;
		public string icon;
		public string text;

		public Help(){}

		public Help(string unitsAreBesidesBuildingsEssentialToTheGameWhenClickedTheyCanBeMovedWithTheArrowKeysWhichCostsActionPointsAPYouCanAlsoPerformVariousActionsWhichCostsAPAtTheEndOfEachRoundTheAPWillBeRefilled, string name, string icon, string text){
			this.unitsAreBesidesBuildingsEssentialToTheGameWhenClickedTheyCanBeMovedWithTheArrowKeysWhichCostsActionPointsAPYouCanAlsoPerformVariousActionsWhichCostsAPAtTheEndOfEachRoundTheAPWillBeRefilled = unitsAreBesidesBuildingsEssentialToTheGameWhenClickedTheyCanBeMovedWithTheArrowKeysWhichCostsActionPointsAPYouCanAlsoPerformVariousActionsWhichCostsAPAtTheEndOfEachRoundTheAPWillBeRefilled;
			this.name = name;
			this.icon = icon;
			this.text = text;
		}
	}
	public class SheetHelp: IEnumerable{
		public System.DateTime updated = new System.DateTime(2020,3,28,14,4,30);
		public readonly string[] labels = new string[]{"Units are, besides buildings, essential to the game. When clicked, they can be moved with the arrow keys, which costs action points (AP). You can also perform various actions, which costs AP. At the end of each round, the AP will be refilled.","name","icon","text"};
		private Help[] _rows = new Help[11];
		public void Init() {
			_rows = new Help[]{
					new Help("moveUnit","Move the units","base:move","At the moment, you can only move the units with the arrow keys, if they have enough AP. Every Round the AP will be refreshed."),
					new Help("moveCamera","Move the camera","base:map","To move the camera, please use WASD on the keyboard."),
					new Help("general","General","logo","Hello! \nNice to meet you, i'm Sven and develop 9 Nations. It is a tuned-based strategy game. You have to explore the area and develop your kingdoms.\n"),
					new Help("beta","Warning","ui:debug","Hello,\nThis version starts a different kind of play. Your nation will later develop elements, at the moment two. In this state the game does not contains all features at the moment. This means that you can encounter bugs, crashes, incomplete and unpolished features, etc. Also except balancing issuses.\nIf you have found any mistakes, bugs oder you have questions, please use the feedback button. I'm happy to hear from you. \nGreetings Sven"),
					new Help("privacy","Privacy Policy","internet","Please take a look at the website https://9nations.de/privacy-policy/privacy-policy-in-game/"),
					new Help("new","Whats new?","base:new","Hello,\nthis update brings two elements to play. To understand 9 nations, play the tutorial. You can find it in the campaign menu.\nYou can find the new features on the website https://9nations.de"),
					new Help("research","Research","magic:research","Since you never know what you will discover during research, research at 9N also functions somewhat differently. You choose different areas or a combination of them and start researching. The more areas you combine, the longer the research will take, but the greater the chance of researching something. For example, if I only do research in the area of life, you will discover the larger village hall. If you combine life and wood, you can also discover the larger house."),
					new Help("unit","Units","base:train","Units are, besides buildings, essential to the game. When clicked, they can be moved with the arrow keys, which costs action points (AP). You can also perform various actions, which costs AP. At the end of each round, the AP will be refilled.\nUnits are always supplied by the nearest city, if available, i.e. they use the resources of this city."),
					new Help("building","Building","base:build","Buildings are, besides the units, basic to the game. If they are clicked, actions can be triggered, which costs action points (AP). At the end of each round, the AP is replenished.\nBuildings are always part of a city, the closest city is automatically selected during construction. "),
					new Help("town","Town","base:foundTown","A city is a collection of buildings, with shared resources. A city also manages all units that are in its area.\nCities always have a center, which is represented by the town hall. The city can be levelled if the town hall is upgraded.  "),
					new Help("developElement","Develop your nation","Icons/magic:air","You can use a ready-made nation or develop your own. If you want to develop your own, you have to decide with which element you want to develop your nation. Each element gives you different units, buildings, abilities and access to other elements. In total you can develop in 4 elements.")
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
					if( _rows[i].unitsAreBesidesBuildingsEssentialToTheGameWhenClickedTheyCanBeMovedWithTheArrowKeysWhichCostsActionPointsAPYouCanAlsoPerformVariousActionsWhichCostsAPAtTheEndOfEachRoundTheAPWillBeRefilled == id){ return _rows[i]; }
				}
				return null;
			}
		}
		public bool ContainsKey(string key){
			for (int i = 0; i < _rows.Length; i++) {
				if( _rows[i].unitsAreBesidesBuildingsEssentialToTheGameWhenClickedTheyCanBeMovedWithTheArrowKeysWhichCostsActionPointsAPYouCanAlsoPerformVariousActionsWhichCostsAPAtTheEndOfEachRoundTheAPWillBeRefilled == key){ return true; }
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
		public Help research{	get{ return _rows[6]; } }
		public Help unit{	get{ return _rows[7]; } }
		public Help building{	get{ return _rows[8]; } }
		public Help town{	get{ return _rows[9]; } }
		public Help developElement{	get{ return _rows[10]; } }

	}
}