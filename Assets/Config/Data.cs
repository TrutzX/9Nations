
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
	//Sheet SheetGameButton
	public static DataTypes.SheetGameButton gameButton = new DataTypes.SheetGameButton();
	//Sheet SheetInputKey
	public static DataTypes.SheetInputKey inputKey = new DataTypes.SheetInputKey();
	//Sheet SheetResearch
	public static DataTypes.SheetResearch research = new DataTypes.SheetResearch();
	//Sheet SheetElement
	public static DataTypes.SheetElement element = new DataTypes.SheetElement();
	//Sheet SheetNAction
	public static DataTypes.SheetNAction nAction = new DataTypes.SheetNAction();
	//Sheet SheetUnit
	public static DataTypes.SheetUnit unit = new DataTypes.SheetUnit();
	//Sheet SheetCampaign
	public static DataTypes.SheetCampaign campaign = new DataTypes.SheetCampaign();
	//Sheet SheetScenario
	public static DataTypes.SheetScenario scenario = new DataTypes.SheetScenario();
	//Sheet SheetMapAction
	public static DataTypes.SheetMapAction mapAction = new DataTypes.SheetMapAction();
	//Sheet SheetNTerrain
	public static DataTypes.SheetNTerrain nTerrain = new DataTypes.SheetNTerrain();
	//Sheet SheetNation
	public static DataTypes.SheetNation nation = new DataTypes.SheetNation();
	//Sheet SheetBuilding
	public static DataTypes.SheetBuilding building = new DataTypes.SheetBuilding();
	//Sheet SheetIcons
	public static DataTypes.SheetIcons icons = new DataTypes.SheetIcons();
	//Sheet SheetHelp
	public static DataTypes.SheetHelp help = new DataTypes.SheetHelp();
	static Data(){
		ress.Init(); features.Init(); featurePlayer.Init(); gameButton.Init(); inputKey.Init(); research.Init(); element.Init(); nAction.Init(); unit.Init(); campaign.Init(); scenario.Init(); mapAction.Init(); nTerrain.Init(); nation.Init(); building.Init(); icons.Init(); help.Init(); 
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
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,46);
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
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,47);
		public readonly string[] labels = new string[]{"id","name","icon","scope","standard","type"};
		private Features[] _rows = new Features[7];
		public void Init() {
			_rows = new Features[]{
					new Features("research","Enable Research","","game","TRUE","bool"),
					new Features("debug","Debug","","option","FALSE","bool"),
					new Features("fog","Fog of war","","game","TRUE","bool"),
					new Features("centermouse","Center map on mouse click","","option","FALSE","bool"),
					new Features("xxx","","","xxx","xxxx","bool"),
					new Features("uiScale","Scale the Ui","","xxx","1","scale,0.5,3"),
					new Features("autosave","Save the game automatical","","option","TRUE","bool")
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
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,48);
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
	public partial class GameButton{
		public string id;
		public string name;
		public string icon;
		public string location;
		public string sound;
		public string req1;
		public string req2;

		public GameButton(){}

		public GameButton(string id, string name, string icon, string location, string sound, string req1, string req2){
			this.id = id;
			this.name = name;
			this.icon = icon;
			this.location = location;
			this.sound = sound;
			this.req1 = req1;
			this.req2 = req2;
		}
	}
	public class SheetGameButton: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,48);
		public readonly string[] labels = new string[]{"id","name","icon","location","sound","req1","req2"};
		private GameButton[] _rows = new GameButton[16];
		public void Init() {
			_rows = new GameButton[]{
					new GameButton("endless","Endless game","base:map","title","click","",""),
					new GameButton("campaign","Campaign","base:campaign","title","click","",""),
					new GameButton("research","Research","magic:research","top","click","featureP:research,true",""),
					new GameButton("lexicon","Lexicon","magic:lexicon","top,title","click","",""),
					new GameButton("quest","Quests","base:quest","top","click","questMin:1",""),
					new GameButton("debug","Debug","ui:debug","top","click","feature:debug,true",""),
					new GameButton("towns","Town","base:foundTown","top","click","townMin:1",""),
					new GameButton("nextround","Next Round","icons:NextRound","bottom","click","",""),
					new GameButton("options","Options","base:option","game,title","click","",""),
					new GameButton("save","Save Game","ui:file","game","click","",""),
					new GameButton("load","Load Game","ui:file","game,title","click","",""),
					new GameButton("title","To Title menu","logo","game","click","",""),
					new GameButton("kingdom","Kingdom Overview","logo","top","click","townMin:1",""),
					new GameButton("mainmenu","Main menu","logo","top","click","",""),
					new GameButton("exit","Exit game","logo","title","click","",""),
					new GameButton("nextUnit","Show next Unit","other:nextUnit","bottom","click","","")
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetGameButton t;
			public SheetEnumerator(SheetGameButton t){
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
		public GameButton this[int index]{
			get{
				return _rows[index];
			}
		}
		public GameButton this[string id]{
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
		public GameButton[] ToArray(){
			return _rows;
		}
		public GameButton Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public GameButton endless{	get{ return _rows[0]; } }
		public GameButton campaign{	get{ return _rows[1]; } }
		public GameButton research{	get{ return _rows[2]; } }
		public GameButton lexicon{	get{ return _rows[3]; } }
		public GameButton quest{	get{ return _rows[4]; } }
		public GameButton debug{	get{ return _rows[5]; } }
		public GameButton towns{	get{ return _rows[6]; } }
		public GameButton nextround{	get{ return _rows[7]; } }
		public GameButton options{	get{ return _rows[8]; } }
		public GameButton save{	get{ return _rows[9]; } }
		public GameButton load{	get{ return _rows[10]; } }
		public GameButton title{	get{ return _rows[11]; } }
		public GameButton kingdom{	get{ return _rows[12]; } }
		public GameButton mainmenu{	get{ return _rows[13]; } }
		public GameButton exit{	get{ return _rows[14]; } }
		public GameButton nextUnit{	get{ return _rows[15]; } }

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
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,49);
		public readonly string[] labels = new string[]{"id","key","type","hidden","active"};
		private InputKey[] _rows = new InputKey[33];
		public void Init() {
			_rows = new InputKey[]{
					new InputKey("research","F1","gameButton",false,false),
					new InputKey("lexicon","F2","gameButton",false,false),
					new InputKey("quest","F3","gameButton",false,false),
					new InputKey("debug","F4","gameButton",false,false),
					new InputKey("towns","F5","gameButton",false,false),
					new InputKey("nextround","Space","gameButton",false,false),
					new InputKey("options","O","gameButton",false,false),
					new InputKey("save","F12","gameButton",false,false),
					new InputKey("load","F11","gameButton",false,false),
					new InputKey("kingdom","F6","gameButton",false,false),
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
					new InputKey("zoomCameraIn","E","hidden",true,false),
					new InputKey("zoomCameraOut","Q","hidden",true,false),
					new InputKey("moveLayerTop","R","hidden",true,false),
					new InputKey("moveLayerDown","F","hidden",true,false),
					new InputKey("minimap","M","hidden",true,false)
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
		public InputKey towns{	get{ return _rows[4]; } }
		public InputKey nextround{	get{ return _rows[5]; } }
		public InputKey options{	get{ return _rows[6]; } }
		public InputKey save{	get{ return _rows[7]; } }
		public InputKey load{	get{ return _rows[8]; } }
		public InputKey kingdom{	get{ return _rows[9]; } }
		public InputKey mainmenu{	get{ return _rows[10]; } }
		public InputKey nextUnit{	get{ return _rows[11]; } }
		public InputKey destroy{	get{ return _rows[12]; } }
		public InputKey build{	get{ return _rows[13]; } }
		public InputKey foundTown{	get{ return _rows[14]; } }
		public InputKey train{	get{ return _rows[15]; } }
		public InputKey buildUpgrade{	get{ return _rows[16]; } }
		public InputKey trainUpgrade{	get{ return _rows[17]; } }
		public InputKey trade{	get{ return _rows[18]; } }
		public InputKey sleep{	get{ return _rows[19]; } }
		public InputKey moveUnitEast{	get{ return _rows[20]; } }
		public InputKey moveUnitSouth{	get{ return _rows[21]; } }
		public InputKey moveUnitWest{	get{ return _rows[22]; } }
		public InputKey moveUnitNorth{	get{ return _rows[23]; } }
		public InputKey moveCameraEast{	get{ return _rows[24]; } }
		public InputKey moveCameraSouth{	get{ return _rows[25]; } }
		public InputKey moveCameraWest{	get{ return _rows[26]; } }
		public InputKey moveCameraNorth{	get{ return _rows[27]; } }
		public InputKey zoomCameraIn{	get{ return _rows[28]; } }
		public InputKey zoomCameraOut{	get{ return _rows[29]; } }
		public InputKey moveLayerTop{	get{ return _rows[30]; } }
		public InputKey moveLayerDown{	get{ return _rows[31]; } }
		public InputKey minimap{	get{ return _rows[32]; } }

	}
}
namespace DataTypes{
	public partial class Research{
		public string id;
		public string name;
		public string icon;
		public int reqtownlevel;
		public string reqnation;
		public string req3;
		public string research1;
		public string research2;
		public string research3;
		public string research4;
		public string research5;
		public string research6;
		public string research7;
		public string research8;
		public string research9;
		public string action1;
		public string action2;
		public string action3;

		public Research(){}

		public Research(string id, string name, string icon, int reqtownlevel, string reqnation, string req3, string research1, string research2, string research3, string research4, string research5, string research6, string research7, string research8, string research9, string action1, string action2, string action3){
			this.id = id;
			this.name = name;
			this.icon = icon;
			this.reqtownlevel = reqtownlevel;
			this.reqnation = reqnation;
			this.req3 = req3;
			this.research1 = research1;
			this.research2 = research2;
			this.research3 = research3;
			this.research4 = research4;
			this.research5 = research5;
			this.research6 = research6;
			this.research7 = research7;
			this.research8 = research8;
			this.research9 = research9;
			this.action1 = action1;
			this.action2 = action2;
			this.action3 = action3;
		}
	}
	public class SheetResearch: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,49);
		public readonly string[] labels = new string[]{"id","name","icon","req-townlevel","req-nation","req3","research1","research2","research3","research4","research5","research6","research7","research8","research9","action1","action2","action3"};
		private Research[] _rows = new Research[63];
		public void Init() {
			_rows = new Research[]{
					new Research("nlogger2","Bigger logger","north-way:nlogger2",2,"north","","","","","","metal","wood","","","","","",""),
					new Research("nquarry2","Bigger quarry","north-castle:nquarry2",3,"north","","fire","","","earth","","","","","","","",""),
					new Research("nhunter","Hunter","north-farm:nhunter",1,"north","","","water","","","","","life","","","","",""),
					new Research("ntownhall4","Bigger town hall","north-way:ntownhall4",3,"north","","","","air","","","","life","","energy","","",""),
					new Research("ntownhall3","Town hall","north-way:ntownhall3",2,"north","","","","","","","","life","","energy","","",""),
					new Research("ntownhall2","Bigger village hall","north-tower:ntownhall2",1,"north","","","","","","","","life","","","","",""),
					new Research("ntownhall","Village hall","north-tower:ntownhall",6,"north","","","","","","","","","","","","",""),
					new Research("nsawmill","Sawmill","north-more:nsawmill",2,"north","","","","","","metal","wood","","","energy","","",""),
					new Research("nmarket","Market","addon:nmarket",2,"north","","","","air","","metal","","life","","","","",""),
					new Research("nmine","Mine","north-tower:nmine",2,"north","","","","","earth","metal","","","","","","",""),
					new Research("nworkshop","Workshop","1176",2,"north","","fire","","air","","metal","wood","","","","","",""),
					new Research("nfarm2","Bigger farm","north-farm:nfarm2",3,"north","","","water","air","","","","life","","","","",""),
					new Research("nfarm","Farm","north-farm:nfarm",2,"north","","","water","","","","","life","","","","",""),
					new Research("nfield","Field","north-farm:nfield",2,"north","","","water","","earth","","","","","","","",""),
					new Research("nwall","Town wall","1906",3,"north","","","","","","metal","","","death","energy","","",""),
					new Research("ndockyard","Dockyard","addon:dock",3,"north","","","water","air","","","wood","","","","","",""),
					new Research("nstreet2","Street","1910",3,"north","","","","air","","metal","","life","","","","",""),
					new Research("nstreet","Way","1654",2,"north","","","","","","metal","","life","","","","",""),
					new Research("nbrickfactory","Brick factory","788",3,"north","","fire","","","earth","","wood","","","","","",""),
					new Research("narmoury","Armoury","1210",3,"north","","fire","","","earth","metal","","","death","energy","","",""),
					new Research("nbarracks","Barracks","north-more:nbaracks",3,"north","","fire","","","","metal","","","death","","","",""),
					new Research("ncastle","Castle","north-tower:ncastle",4,"north","","fire","","","","metal","","","death","energy","","",""),
					new Research("npalace","Palace","north-more:nstorage",4,"north","","","","","","metal","","life","death","energy","","",""),
					new Research("nstorage","Storage","north-more:nstorage",2,"north","","","","air","","","wood","","","","","",""),
					new Research("nresearch","Research tower","north-tower:nrtower",1,"north","","fire","water","air","earth","","","","","","","",""),
					new Research("nbridge","Bridge","851",3,"north","","","water","","earth","","","","","","","",""),
					new Research("ntemple2","Bigger circle","addon:ncircle2",3,"north","","","","","","","","life","death","energy","","",""),
					new Research("ntemple","Circle","addon:ncircle",4,"north","","fire","water","","","","","","","","","",""),
					new Research("ncircus","Circus","addon:ncircus",2,"north","","","","","earth","","wood","life","","","","",""),
					new Research("nwoodtower","Wood tower","north-tower:nwoodtower",4,"north","","","","air","","","wood","life","","","","",""),
					new Research("nhouse2","House","north-more:nhouse",2,"north","","","","","","","wood","life","","","","",""),
					new Research("nsettler","Settler","nsettler",3,"north","","","","air","","","wood","life","","","","",""),
					new Research("nworker","Worker","nworker",0,"north","","","","","","","","","","","","",""),
					new Research("nship","Ship","nship",3,"north","","","","","","","","","","","","",""),
					new Research("nexplorer","Explorer","nexplorer",1,"north","","","","","","","","","","","","",""),
					new Research("nmarksman","Marksman","",1,"north","","","","","","","","","","","","",""),
					new Research("","Griffin","",2,"north","","","","","","","","","","","","",""),
					new Research("","Royal Griffin","",2,"north","","","","","","","","","","","","",""),
					new Research("nsoldier","Swordsman","nsoldier",3,"north","","","","","","metal","","","death","","","",""),
					new Research("","Crusader","",3,"north","","","","","","","","","","","","",""),
					new Research("nmonk","Monk","nmonk",3,"north","","","","","","","","","","","","",""),
					new Research("nking","King","nking",1,"north","","","","","","","","","","","","",""),
					new Research("ngeneral","General","ngeneral",4,"north","","","","","","","","","","","","",""),
					new Research("nmage","Mage","nmage",4,"north","","fire","","","","","","","death","energy","","",""),
					new Research("","Angel","",4,"north","","","","","","","","","","","","",""),
					new Research("","Archangel","",4,"north","","","","","","","","","","","","",""),
					new Research("rrope","Rope Factory","819",2,"ranger","","","","","","","wood","","","","","",""),
					new Research("rleaf","Leaf collector","194",1,"ranger","","","","","","","","","","","","",""),
					new Research("rsling","Slingshot workshop","1114",3,"ranger","","","","air","","","wood","","death","","","",""),
					new Research("rplank","Plank creater","743",2,"ranger","","","","","","","wood","","","energy","","",""),
					new Research("rwood","Wood collector","1838",1,"ranger","","","","","","","","","","","","",""),
					new Research("rberry","Berry finder","132",1,"ranger","","","","","","","","","","","","",""),
					new Research("rwapple","Winter apple","958",2,"ranger","","","","","","","wood","life","","","","",""),
					new Research("rhouse","Tree house","1316",1,"ranger","","","","","","","","","","","","",""),
					new Research("rresearch","Mediation","1900",1,"ranger","","","","","","","","","","","","",""),
					new Research("rtrain","Training","1871",3,"ranger","","","","","","","","","","","","",""),
					new Research("rpalast5","Biggest palast","66",4,"ranger","","","","","","","","","","","","",""),
					new Research("rpalast4","Bigger palast","34",3,"ranger","","","","","","","","","","","","",""),
					new Research("rpalast3","Big palast","34",2,"ranger","","","","","","","","","","","","",""),
					new Research("rpalast2","Medium palast","959",1,"ranger","","","","","","","","","","","","",""),
					new Research("rpalast","Small palast","959",1,"ranger","","","","","","","","","","","","",""),
					new Research("ralchemy","Alchemy","1462",4,"ranger","","fire","","","","metal","","","death","energy","","",""),
					new Research("rstreet","Way","1654",2,"ranger","","","","","earth","","wood","","","","","","")
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetResearch t;
			public SheetEnumerator(SheetResearch t){
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
		public Research this[int index]{
			get{
				return _rows[index];
			}
		}
		public Research this[string id]{
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
		public Research[] ToArray(){
			return _rows;
		}
		public Research Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public Research nlogger2{	get{ return _rows[0]; } }
		public Research nquarry2{	get{ return _rows[1]; } }
		public Research nhunter{	get{ return _rows[2]; } }
		public Research ntownhall4{	get{ return _rows[3]; } }
		public Research ntownhall3{	get{ return _rows[4]; } }
		public Research ntownhall2{	get{ return _rows[5]; } }
		public Research ntownhall{	get{ return _rows[6]; } }
		public Research nsawmill{	get{ return _rows[7]; } }
		public Research nmarket{	get{ return _rows[8]; } }
		public Research nmine{	get{ return _rows[9]; } }
		public Research nworkshop{	get{ return _rows[10]; } }
		public Research nfarm2{	get{ return _rows[11]; } }
		public Research nfarm{	get{ return _rows[12]; } }
		public Research nfield{	get{ return _rows[13]; } }
		public Research nwall{	get{ return _rows[14]; } }
		public Research ndockyard{	get{ return _rows[15]; } }
		public Research nstreet2{	get{ return _rows[16]; } }
		public Research nstreet{	get{ return _rows[17]; } }
		public Research nbrickfactory{	get{ return _rows[18]; } }
		public Research narmoury{	get{ return _rows[19]; } }
		public Research nbarracks{	get{ return _rows[20]; } }
		public Research ncastle{	get{ return _rows[21]; } }
		public Research npalace{	get{ return _rows[22]; } }
		public Research nstorage{	get{ return _rows[23]; } }
		public Research nresearch{	get{ return _rows[24]; } }
		public Research nbridge{	get{ return _rows[25]; } }
		public Research ntemple2{	get{ return _rows[26]; } }
		public Research ntemple{	get{ return _rows[27]; } }
		public Research ncircus{	get{ return _rows[28]; } }
		public Research nwoodtower{	get{ return _rows[29]; } }
		public Research nhouse2{	get{ return _rows[30]; } }
		public Research nsettler{	get{ return _rows[31]; } }
		public Research nworker{	get{ return _rows[32]; } }
		public Research nship{	get{ return _rows[33]; } }
		public Research nexplorer{	get{ return _rows[34]; } }
		public Research nmarksman{	get{ return _rows[35]; } }
		public Research unkown{	get{ return _rows[36]; } }
		public Research unkown01{	get{ return _rows[37]; } }
		public Research nsoldier{	get{ return _rows[38]; } }
		public Research unkown02{	get{ return _rows[39]; } }
		public Research nmonk{	get{ return _rows[40]; } }
		public Research nking{	get{ return _rows[41]; } }
		public Research ngeneral{	get{ return _rows[42]; } }
		public Research nmage{	get{ return _rows[43]; } }
		public Research unkown03{	get{ return _rows[44]; } }
		public Research unkown04{	get{ return _rows[45]; } }
		public Research rrope{	get{ return _rows[46]; } }
		public Research rleaf{	get{ return _rows[47]; } }
		public Research rsling{	get{ return _rows[48]; } }
		public Research rplank{	get{ return _rows[49]; } }
		public Research rwood{	get{ return _rows[50]; } }
		public Research rberry{	get{ return _rows[51]; } }
		public Research rwapple{	get{ return _rows[52]; } }
		public Research rhouse{	get{ return _rows[53]; } }
		public Research rresearch{	get{ return _rows[54]; } }
		public Research rtrain{	get{ return _rows[55]; } }
		public Research rpalast5{	get{ return _rows[56]; } }
		public Research rpalast4{	get{ return _rows[57]; } }
		public Research rpalast3{	get{ return _rows[58]; } }
		public Research rpalast2{	get{ return _rows[59]; } }
		public Research rpalast{	get{ return _rows[60]; } }
		public Research ralchemy{	get{ return _rows[61]; } }
		public Research rstreet{	get{ return _rows[62]; } }

	}
}
namespace DataTypes{
	public partial class Element{
		public string id;
		public string name;
		public string icon;

		public Element(){}

		public Element(string id, string name, string icon){
			this.id = id;
			this.name = name;
			this.icon = icon;
		}
	}
	public class SheetElement: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,50);
		public readonly string[] labels = new string[]{"id","name","icon"};
		private Element[] _rows = new Element[9];
		public void Init() {
			_rows = new Element[]{
					new Element("fire","Fire","Icons/magic:fire"),
					new Element("air","Air","Icons/magic:air"),
					new Element("death","Death","Icons/base:destroy"),
					new Element("earth","Earth","Icons/magic:earth"),
					new Element("energy","Energy","Icons/magic:energy"),
					new Element("life","Life","Icons/magic:life"),
					new Element("metal","Metal","Icons/mining:metal"),
					new Element("water","Water","Icons/magic:water"),
					new Element("wood","Wood","Icons/base:wood")
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
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,50);
		public readonly string[] labels = new string[]{"id","name","desc","cost","icon","activeMapElement","req1","req2","useUnderConstruction","onlyOwner","sound","persistent"};
		private NAction[] _rows = new NAction[17];
		public void Init() {
			_rows = new NAction[]{
					new NAction("destroy","Destroy it","Destroy it",0,"base:destroy",true,"","",true,true,"click",false),
					new NAction("build","Build","Construct a new Building",5,"base:build",true,"empty:building","townMin:1",false,true,"click",false),
					new NAction("foundTownFirst","Found your first town","Found your first town",10,"base:foundTown",true,"town:<0","",false,true,"click",false),
					new NAction("foundTown","Found a new town","Found a new town",10,"base:foundTown",true,"","",false,true,"click",false),
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
					new NAction("move","Move","Move this unit",0,"base:move",true,"","",false,true,"click",false),
					new NAction("moveTo","Move to point","Move the unit to a specific point",0,"logo",true,"","",false,true,"click",true),
					new NAction("attack","Attack","Attack a unit",10,"logo",true,"","",false,true,"click",false)
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
		public NAction attack{	get{ return _rows[16]; } }

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
		public string mapaction1;
		public string mapaction2;
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

		public Unit(string id, string name, string file, string cat, string movetyp, int atk, int def, int dam_min, int dam_max, int hp, int ap, int visible, int buildtime, int reqbuildtownlevel, string reqbuildnation, string reqbuild1, string reqbuild2, string reqbuild3, string cost1, string cost2, string cost3, string cost4, string cost5, string cost6, string cost7, string reqproduce1, string reqproduce2, string reqproduce3, string produce1, string produce2, string produce3, string research, string action1, string action2, string action3, string action4, string action5, string mapaction1, string mapaction2, int embark, string actproduce, string aproduce, int gift, string build, string foundTown, string reqdate, int actionTerrainRemoveforest, int actionTerrainAddgrass, string actionalchemyA){
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
			this.mapaction1 = mapaction1;
			this.mapaction2 = mapaction2;
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
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,51);
		public readonly string[] labels = new string[]{"id","name","file","cat","movetyp","atk","def","dam_min","dam_max","hp","ap","visible","buildtime","req-build-townlevel","req-build-nation","req-build1","req-build2","req-build3","cost1","cost2","cost3","cost4","cost5","cost6","cost7","req-produce1","req-produce2","req-produce3","produce1","produce2","produce3","research","action1","action2","action3","action4","action5","mapaction1","mapaction2","embark","act-produce","aproduce","gift","build","foundTown","req-date","actionTerrainRemove-forest","actionTerrainAdd-grass","action-alchemyA"};
		private Unit[] _rows = new Unit[27];
		public void Init() {
			_rows = new Unit[]{
					new Unit("nsettler","Settler","nsettler","prod","walk",0,5,1,3,10,10,0,9,3,"north","","","research:nsettler","wood:15","food:60","stone:8","tool:10","gold:2","worker:6","","resMin:wood,1","resMin:food,1","","wood:-1","food:-1","","","","","","foundTown","","","",0,"food-2","",0,"","1","",0,0,""),
					new Unit("nworker","Worker","nworker","prod","walk",0,5,1,3,10,15,-4,1,0,"north","","","","wood:2","food:5","","","","worker:1","","","","","","","","","destroy","sleep","build","","move","","",0,"wood-3","stone-3",0,"1","","",1,0,""),
					new Unit("nship","Ship","nship","explo","swim",6,5,2,3,10,20,2,6,3,"north","","","","plank:10","","tool:3","","","worker:2","","","","","","","","","","","","","","attack","",2,"","",0,"bridge","","",0,0,""),
					new Unit("nexplorer","Explorer","nexplorer","explo","walk",6,3,2,3,10,10,3,3,1,"north","","","","wood:2","food:10","stone:1","","","worker:1","","","","","","","","","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("nmarksman","Marksman","","","walk",6,3,2,3,10,6,1,1,1,"north","","disabled","research:nmarksman","","","","","","","","","","","","","","","","","","","","attack","",0,"","",0,"","","",0,0,""),
					new Unit("nsoldier","Swordsman","nsoldier","war","walk",10,12,6,9,35,10,2,6,3,"north","","","research:nsoldier","","","weapon:1","","gold:2","worker:1","","","resMin:food,1","","","food:-1","","","destroy","sleep","","","move","attack","",0,"","",0,"","","",0,0,""),
					new Unit("nmonk","Monk","nmonk","","walk",12,7,10,12,30,10,1,1,3,"north","","","research:nmonk","","","","","","","","","","","","","","","","","","","","heal","",0,"","",0,"","","",0,0,""),
					new Unit("nking","King","nking","general","walk",12,10,10,12,30,10,1,1,1,"north","maxUnitPlayer:nking,0","","","","","","","","","","","","","","","","","","sleep","build","foundTownFirst","move","attack","heal",0,"food-3","",0,"1","first","",0,0,""),
					new Unit("ngeneral","General","ngeneral","","walk",15,15,15,25,100,10,1,1,4,"north","","","research:ngeneral","","","","","","","","","","","","","","","","","","","","attack","",0,"","",0,"","","",0,0,""),
					new Unit("nmage","Mage","nmage","war","walk",16,16,20,25,100,15,2,4,4,"north","","","research:nmage","","","weapon:2","","gold:6","worker:1","","","resMin:food,2","","","food:-2","","","","","","","","attack","",0,"","",0,"","","",0,0,"gold-4"),
					new Unit("rworker","Worker","rgnom","prod","walk",0,3,2,3,8,10,0,1,1,"ranger","","disabled","","wood:2","food:5","tool:2","","","worker:1","","","","","","","","","","","","","","","",0,"wood-3","leaf-3",0,"1","","",0,1,""),
					new Unit("rexplorer","Explorer","units1.png-4","explo","fly",6,4,2,3,10,15,3,3,1,"ranger","","disabled","","wood:2","food:10","weapon:5","","gold:1","worker:1","","","","","","","","","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("rnature","Nature","units1.png-4","general","walk",6,7,2,4,20,5,2,1,1,"ranger","maxUnitPlayer:rnature,0","disabled","","","","","","","","","","","","","","","","","","","","","","",0,"food-3","",0,"1","first","",0,1,""),
					new Unit("rfox","Fox","units1.png-91","war","walk",7,7,2,4,20,10,1,4,1,"ranger","","disabled","","wood:2","food:6","tomato:1","","","","","","","","","","","wood","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("rgoldmonkey","Gold Monkey","units2.png-36","war","fly",18,18,40,50,180,10,1,2,4,"ranger","","disabled","","","food:2","","leaf:3","","worker:1","","","","","","","","air,metal,energy","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("rmonkey","Monkey","units2.png-24","war","walk",9,5,3,5,15,6,1,8,2,"ranger","","disabled","","","","peanut:1","leaf:6","","","","","","","","","","air,wood","trainUpgrade:rgoldmonkey","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("rrabbit","Rabbit","units1.png-76","prod","walk",9,5,3,5,15,7,1,5,2,"ranger","","disabled","","","food:4","carrot:1","leaf:2","","","","","","","","","","earth,wood","","","","","","","",0,"food-5","",0,"","","",0,0,""),
					new Unit("rcornfairy","Corn Fairy","units1.png-46","prod","fly",15,14,18,22,90,7,2,10,4,"ranger","","disabled","","","","corn:3","","","","","","","","","","","air,wood,life","trainUpgrade:rfairy","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("rfairy","Fairy","units1.png-15","war","fly",9,8,5,9,30,8,2,6,2,"ranger","","disabled","","wood:15","food:15","tool:10","stone:1","gold:1","worker:1","seed:3","","resMin:food,1","","","food:-1","","air,death","","","","","","","",0,"food-2","",0,"","","",0,0,""),
					new Unit("rslime","Slime","units2.png-19","war","swim",9,10,5,9,30,12,1,3,3,"ranger","","disabled","","","","","rope:3","gold:1","","","","","","","","","water,air,life","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("rsnake","Snake","units1.png-81","war","walk",9,12,10,14,55,3,1,4,3,"ranger","","disabled","","","","","","","","","","","","","","","","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("rgolm","Golm","units1.png-16","war","walk",9,12,10,14,65,20,2,9,3,"ranger","","disabled","","","food:25","tool:3","stone:3","gold:3","worker:2","","","resMin:food,2","","","food:-2","","wood,death,energy","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("relf","Elf","units1.png-6","prod","walk",15,14,18,22,110,9,2,12,2,"ranger","maxUnitPlayer:relf,0","disabled","","","","","","gold:1","worker:1","","","","","","","","life","","","","","","","",0,"","",1,"","","357-361",0,0,""),
					new Unit("rpegasus","Pegasus","units1.png-56","war","fly",27,27,40,50,250,20,3,9,5,"ranger","","disabled","","cucumber:2","","","rope:3","gold:1","worker:1","","","resMin:gold,1","","","gold:-1","","air,wood,life","","","","","","","",1,"","",0,"","","",0,0,""),
					new Unit("dghost","Ghost","","","fly",7,7,3,5,18,5,0,5,0,"dead","","","","","","","","","","","","","","","","","","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("dnightking","Night King","units1.png-20","","WALK",13,10,11,15,40,7,0,10,0,"dead","maxUnitPlayer:dnightking,0","","","","","","","","","","","","","","","","","","","","","","","",0,"","",0,"","","",0,0,""),
					new Unit("dblackknight","Black Knight","units2.png-52","","WALK",16,16,15,30,120,7,0,11,0,"dead","","","","","","","","","","","","","","","","","","","","","","","","",0,"","",0,"","","",0,0,"")
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
	public partial class Campaign{
		public string id;
		public string name;
		public string icon;
		public string desc;
		public bool progress;
		public bool hidden;

		public Campaign(){}

		public Campaign(string id, string name, string icon, string desc, bool progress, bool hidden){
			this.id = id;
			this.name = name;
			this.icon = icon;
			this.desc = desc;
			this.progress = progress;
			this.hidden = hidden;
		}
	}
	public class SheetCampaign: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,52);
		public readonly string[] labels = new string[]{"id","name","icon","desc","progress","hidden"};
		private Campaign[] _rows = new Campaign[2];
		public void Init() {
			_rows = new Campaign[]{
					new Campaign("tutorial","Tutorial","ui:tutorial","You will learn the basics of 9 Nations.",false,false),
					new Campaign("single","Single Scenarios","magic:single","Play different single scenarios.",false,true)
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetCampaign t;
			public SheetEnumerator(SheetCampaign t){
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
		public Campaign this[int index]{
			get{
				return _rows[index];
			}
		}
		public Campaign this[string id]{
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
		public Campaign[] ToArray(){
			return _rows;
		}
		public Campaign Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public Campaign tutorial{	get{ return _rows[0]; } }
		public Campaign single{	get{ return _rows[1]; } }

	}
}
namespace DataTypes{
	public partial class Scenario{
		public string id;
		public string name;
		public string icon;
		public string desc;
		public string campaign;
		public bool hidden;
		public string map;

		public Scenario(){}

		public Scenario(string id, string name, string icon, string desc, string campaign, bool hidden, string map){
			this.id = id;
			this.name = name;
			this.icon = icon;
			this.desc = desc;
			this.campaign = campaign;
			this.hidden = hidden;
			this.map = map;
		}
	}
	public class SheetScenario: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,52);
		public readonly string[] labels = new string[]{"id","name","icon","desc","campaign","hidden","map"};
		private Scenario[] _rows = new Scenario[4];
		public void Init() {
			_rows = new Scenario[]{
					new Scenario("tutorialbasic","Basics","ui:tutorial","You learn to move your units, found a town and build some buildings.","tutorial",false,"field6"),
					new Scenario("single","Single Scenarios","magic:single","Please different single scenarios.","single",false,"field6"),
					new Scenario("debug","","","","",true,"field6"),
					new Scenario("endless","","","","",true,"")
				};
		}
			
		public IEnumerator GetEnumerator(){
			return new SheetEnumerator(this);
		}
		private class SheetEnumerator : IEnumerator{
			private int idx = -1;
			private SheetScenario t;
			public SheetEnumerator(SheetScenario t){
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
		public Scenario this[int index]{
			get{
				return _rows[index];
			}
		}
		public Scenario this[string id]{
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
		public Scenario[] ToArray(){
			return _rows;
		}
		public Scenario Random() {
			return _rows[ UnityEngine.Random.Range(0, _rows.Length) ];
		}

		public Scenario tutorialbasic{	get{ return _rows[0]; } }
		public Scenario single{	get{ return _rows[1]; } }
		public Scenario debug{	get{ return _rows[2]; } }
		public Scenario endless{	get{ return _rows[3]; } }

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
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,53);
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
	public partial class NTerrain{
		public string id;
		public string name;
		public string desc;
		public string sound;
		public string icon;
		public int walk;
		public int fly;
		public int swim;
		public int buildtime;
		public int visible;
		public int tileid;

		public NTerrain(){}

		public NTerrain(string id, string name, string desc, string sound, string icon, int walk, int fly, int swim, int buildtime, int visible, int tileid){
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
			this.tileid = tileid;
		}
	}
	public class SheetNTerrain: IEnumerable{
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,53);
		public readonly string[] labels = new string[]{"id","name","desc","sound","icon","walk","fly","swim","buildtime","visible","tileid"};
		private NTerrain[] _rows = new NTerrain[18];
		public void Init() {
			_rows = new NTerrain[]{
					new NTerrain("border","Map Border","Not passable","","",0,0,0,0,0,0),
					new NTerrain("fog","Fog","Not explored","","",0,0,0,0,0,0),
					new NTerrain("unknown","Dark Terrain","Not visible","","394",0,0,0,0,0,1),
					new NTerrain("plains","Plains","default terrain","","0",10,5,0,0,0,0),
					new NTerrain("underground","Underground","default underground","","",10,0,0,0,0,0),
					new NTerrain("tundra","Tundra","cold basic","","",5,0,0,0,0,0),
					new NTerrain("sand","Sand","Hot basic","","",5,0,0,0,0,0),
					new NTerrain("ocean","Ocean","","","16",0,10,10,0,0,0),
					new NTerrain("water","Fresh Water","","","",0,5,5,0,0,4),
					new NTerrain("grass","Grass","","bite-small3","Icons/terrain:grass",10,5,0,0,0,3),
					new NTerrain("forest","Forest","","interface5","Icons/terrain:forest",10,5,0,25,-1,2),
					new NTerrain("hill","Hill","","","Icons/terrain:hill",10,5,0,50,1,6),
					new NTerrain("mountain","Mountain","","metalPot3","204",15,7,0,100,2,5),
					new NTerrain("clouds","Clouds","","","",0,0,0,0,0,0),
					new NTerrain("marsh","Marsh","","","",15,5,10,0,0,0),
					new NTerrain("lava","Lava","","","",0,0,0,0,0,0),
					new NTerrain("snow","Snow","","","",0,0,0,0,0,0),
					new NTerrain("ice","Ice","","","",0,0,0,0,0,0)
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
		public NTerrain forest{	get{ return _rows[10]; } }
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
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,54);
		public readonly string[] labels = new string[]{"id","name","hidden","desc","icon","leader","townhall","townlevel2","townlevel3","townlevel4","townLevelChangeable-food","townLevelChangeable-gold","terrain-forrest","#origin","ethos","researchElement","hometerrain","townNameGenerator","townLevelName1","townLevelName2","townLevelName3","townLevelName4"};
		private Nation[] _rows = new Nation[9];
		public void Init() {
			_rows = new Nation[]{
					new Nation("north","North",false,"Default nation",76,"nking","ntownhall","faith","safety","culture",0.2f,0.1f,0,"n","good","metal","grass","north","Village","Bigger village","Town","Bigger town"),
					new Nation("ranger","Rangers",true,"Nation under construction. Not really usefull at the moment.",584,"rnature","rpalast","","","",0.2f,-0.1f,-5,"magic","good","life","woods","elf","","","Kingdom","Bigger kingdom"),
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
		public string reqbuild4;
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
		public string actiononce1;
		public string actiononce2;
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

		public Building(string id, string name, string file, int width, int height, int reqbuildtownlevel, string reqbuildnation, string reqbuild1, string reqbuild2, string reqbuild3, string reqbuild4, string cat, int atk, int def, string connected, int hp, int ap, int visible, int buildtime, string cost1, string cost2, string cost3, string cost4, string cost5, string cost6, string reqproduce1, string reqproduce2, string reqproduce3, string reqproduce4, string reqproduce5, string produce1, string produce2, string produce3, string produce4, string produce5, string action1, string action2, string action3, string action4, string action5, string produceonce1, string produceonce2, string produceonce3, string produceonce4, string produceonce5, string produceonce6, string actiononce1, string actiononce2, string actionalchemyP, int actiondestroyTown, int actionsendRess, string reqscenconf, string reqterrain, string reqmax, string reqmin, int passiveActionupgradeRangeLimit, int passiveActionupgradeBuildingLimit, int passiveActionstorage, int passiveActionupgradeTownLimit, int passiveActionupgradeTownLevel, string passiveActionenablePlayerFeature, int passPublicwalk, int passOwnerwalk, int passOwnerswim){
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
			this.reqbuild4 = reqbuild4;
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
			this.actiononce1 = actiononce1;
			this.actiononce2 = actiononce2;
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
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,54);
		public readonly string[] labels = new string[]{"id","name","file","width","height","req-build-townlevel","req-build-nation","req-build1","req-build2","req-build3","req-build4","cat","atk","def","connected","hp","ap","visible","buildtime","cost1","cost2","cost3","cost4","cost5","cost6","req-produce1","req-produce2","req-produce3","req-produce4","req-produce5","produce1","produce2","produce3","produce4","produce5","action1","action2","action3","action4","action5","produceonce1","produceonce2","produceonce3","produceonce4","produceonce5","produceonce6","actiononce1","actiononce2","action-alchemyP","action-destroyTown","action-sendRess","req-scenconf","req-terrain","req-max","req-min","passiveAction-upgradeRangeLimit","passiveAction-upgradeBuildingLimit","passiveAction-storage","passiveAction-upgradeTownLimit","passiveAction-upgradeTownLevel","passiveAction-enablePlayerFeature","passPublic-walk","passOwner-walk","passOwner-swim"};
		private Building[] _rows = new Building[53];
		public void Init() {
			_rows = new Building[]{
					new Building("nlogger2","Bigger logger","north-way:nlogger2",1,1,2,"north","terrain:forest","field:building,nlogger","research:nlogger2","","prod",0,0,"",20,5,2,5,"wood:10","stone:4","","worker:2","tool:2","","","","","","","wood:6","","","","","","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nlogger","Logger","north-castle:nlogger",1,1,1,"north","terrain:forest","","","","prod",0,0,"",5,5,1,2,"wood:2","stone:","","worker:1","","","","","","","","wood:2","","","","","destroy","buildUpgrade:nlogger2","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nquarry2","Bigger quarry","north-castle:nquarry2",1,1,3,"north","terrain:hill,mountain","field:building,nquarry","research:nquarry2","","prod",0,0,"",30,5,2,7,"wood:9","stone:6","plank:2","worker:2","tool:2","","","","","","","stone:9","","","","","","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nquarry","Quarry","north-castle:nquarry",1,1,1,"north","terrain:hill,mountain","","","","prod",0,0,"",10,5,1,2,"wood:3","","","worker:1","tool:1","","","","","","","stone:3","","","","","destroy","buildUpgrade:nquarry2","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nhunter","Hunter","north-farm:nhunter",1,1,1,"north","terrain:forest,grass","","","","food",0,0,"",5,5,1,3,"wood:4","stone:1","","worker:1","tool:1","","","","","","","food:3","","","","","destroy","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nfisher","Fisher","north-farm:nfisher",1,1,1,"north","terrainNear:water","","","","food",0,0,"",5,5,1,4,"wood:2","stone:2","","worker:1","tool:1","","","","","","","food:4","","","","","destroy","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("ntownhall4","Bigger town hall","north-way:ntownhall4",2,1,3,"north","field:building,ntownhall3","","research:ntownhall4","","needs",0,0,"",100,5,5,10,"","","plank:8","brick:15","","","","","","","","gold:1","","","","","","train:nworker,nexplorer,nsettler","","","","culture:10","","","","","","","","",1,1,"","","","",5,70,300,2,1,"",0,0,0),
					new Building("ntownhall3","Town hall","north-way:ntownhall3",1,1,2,"north","field:building,ntownhall2","","research:ntownhall3","","needs",0,0,"",50,5,4,7,"wood:4","stone:24","plank:4","","","","","","","","","gold:1","","","","","","train:nworker,nexplorer,nsettler","buildUpgrade:ntownhall4","","","","","","","","","","","",1,1,"","","","",5,45,200,1,1,"",0,0,0),
					new Building("ntownhall2","Bigger village hall","north-tower:ntownhall2",1,1,1,"north","field:building,ntownhall","","research:ntownhall2","","needs",0,0,"",25,5,3,4,"wood:4","stone:6","","","","","","","","","","gold:1","","","","","","train:nworker,nexplorer","buildUpgrade:ntownhall3","","","","","","","","","","","",1,0,"","","","",5,25,200,0,1,"",0,0,0),
					new Building("ntownhall","Village hall","north-tower:ntownhall",1,1,1,"north","","building:<0,ntownhall","research:ntownhall","","needs",0,0,"",10,5,2,1,"","","","","","","","","","","","gold:1","","","","","","train:nworker,nexplorer","buildUpgrade:ntownhall2","trade:buy,tools","","food:20","stone:8","wood:15","tool:10","worker:6","workermax:6","","","",1,0,"","","","",0,10,300,0,0,"",0,0,0),
					new Building("nsawmill","Sawmill","north-more:nsawmill",1,1,2,"north","terrainNear:water","","research:nsawmill","","prod",0,0,"",20,5,1,5,"wood:20","stone:10","","worker:1","tool:3","","daytime:not-night","resMin:wood,2","","","","wood:-2","plank:1","","","","destroy","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nmarket","Market","addon:nmarket",1,1,2,"north","townNear","","research:nmarket","","needs",0,0,"",20,5,1,7,"wood:24","stone:6","","worker:1","gold:3","","","","","","","gold:1","","","","","destroy","","trade","","","culture:10","","","","","","","","",0,1,"","","2-nmarket-false","",0,0,0,0,0,"",0,0,0),
					new Building("nmine","Mine","north-tower:nmine",1,1,2,"north","terrain:mountain","","research:nmine","","prod",0,0,"",20,5,0,7,"wood:12","stone:8","plank:3","worker:1","gold:1","","","","","","","ore:1","","","","","destroy","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nworkshop","Workshop","1176",0,0,2,"north","townNear","","research:nworkshop","","prod",0,0,"",20,5,1,5,"wood:8","stone:20","plank:4","worker:2","","","daytime:not-night","resMin:wood,1","resMin:ore,1","","","wood:-1","ore:-1","tool:1","","","destroy","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nfarm2","Bigger farm","north-farm:nfarm2",2,1,3,"north","terrain:grass","field:building,nfarm","research:nfarm2","","food",0,0,"",30,5,1,7,"wood:12","stone:9","plank:5","worker:2","tool:3","","","","","","","","","","","","destroy","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nfarm","Farm","north-farm:nfarm",1,1,2,"north","terrain:grass","","research:nfarm","","food",0,0,"",20,5,1,5,"wood:12","stone:8","","worker:1","tool:2","","season:not-winter","","","","","food:5","","","","","destroy","buildUpgrade:nfarm2","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nfield","Field","north-farm:nfield",1,1,2,"north","terrain:grass","","research:nfield","","food",0,0,"",1,5,1,2,"","","plank:1","","","","season:not-winter","","","","","food:3","","","","","destroy","","","","","","","","","","","","","",0,0,"","","","1-nfarm2-false",0,0,0,0,0,"",0,0,0),
					new Building("nwall","Town wall","1906",0,0,3,"north","","","research:nwall","disabled","war",0,0,"wall",100,5,1,4,"","stone:3","","brick:3","","","","","","","","","","","","","destroy","","","","","culture:10","","","","","safety:3","","","",0,0,"","","","",0,1,0,0,0,"",0,0,0),
					new Building("ndockyard","Dockyard","addon:dock",1,1,3,"north","terrainNear:water","","research:ndockyard","","explo",0,0,"",30,5,1,7,"worker:2","stone:6","plank:3","brick:1","tool:2","","","","","","","","","","","","destroy","train:nship","trade","","","","","","","","safety:10","","","",0,1,"","","","",0,0,0,0,0,"",0,0,5),
					new Building("nstreet2","Street","1910",0,0,3,"north","townNear","field:building,nstreet","research:nstreet2","disabled","explo",0,0,"wall",30,5,1,7,"","stone:5","","brick:1","gold:2","","","","","","","","","","","","","","","","","","","","","","","","","",0,0,"","","","",0,1,0,0,0,"",-2,2,0),
					new Building("nstreet","Way","1654",0,0,2,"north","townNear","","research:nstreet","disabled","explo",0,0,"wall",20,5,1,4,"wood:3","","","","gold:1","","","","","","","","","","","","destroy","buildUpgrade:nstreet2","","","","","","","","","","","","",0,0,"","","","",0,1,0,0,0,"",-2,-2,0),
					new Building("nbrickfactory","Brick factory","788",0,0,3,"north","townNear","","research:nbrickfactory","","prod",0,0,"",30,5,1,7,"worker:2","stone:36","plank:4","","tool:3","","daytime:not-night","resMin:stone,2","","","","stone:-2","brick:1","","","","destroy","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("narmoury","Armoury","1210",0,0,3,"north","townNear","","research:narmoury","disabled","war",0,0,"",30,5,1,7,"worker:3","stone:13","plank:9","brick:7","tool:4","","daytime:not-night","resMin:wood,3","resMin:tool,1","resMin:ore,2","","wood:-3","tool:-1","ore:-2","weapon:1","","destroy","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nbarracks","Barracks","north-more:nbaracks",1,1,3,"north","townNear","","research:nbarracks","","war",0,0,"",60,5,1,7,"worker:2","stone:7","plank:5","brick:9","tool:3","","","","","","","","","","","","destroy","train","","","","","","","","","safety:20","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("ncastle","Castle","north-tower:ncastle",2,2,4,"north","townNear","","research:ncastle","","war",5,1,"",100,5,1,9,"worker:3","stone:14","plank:5","brick:12","tool:6","","","","","","","","","","","","destroy","","","","","","","","","","safety:30","","","",0,0,"","","0-ncastle-true","",5,5,0,1,2,"",0,0,0),
					new Building("npalace","Palace","north-more:nstorage",2,2,4,"north","townNear","building:<0,npalace","research:npalace","","deco",0,0,"",40,5,1,9,"worker:3","gold:10","plank:10","brick:10","tool:10","","","","","","","research:2","","","","","destroy","","","","","culture:40","","","","faith:10","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nstorage","Storage","north-more:nstorage",1,1,2,"north","townNear","","research:nstorage","","general",0,0,"",20,5,1,5,"wood:24","stone:10","","","worker:1","","","","","","","","","","","","destroy","","","","","","","","","","","","","",0,0,"","","","",0,0,300,0,0,"",0,0,0),
					new Building("nlibrary","Library","north-tower:nresearcher",1,1,1,"north","townNear","feature:research,true","","","general",0,0,"",10,5,1,4,"wood:1","stone:4","","","worker:1","","","","","","","research:1","","","","","destroy","","","","","","","","","","","featureP:research,true","","",0,0,"RESEARCH","","","",0,0,0,0,0,"RESEARCH",0,0,0),
					new Building("nresearch","Research tower","north-tower:nrtower",1,2,3,"north","townNear","feature:research,true","research:nresearch","","general",0,0,"",30,5,1,7,"wood:9","stone:6","","brick:6","worker:2","","","resMin:gold,3","","","","research:3","gold:-1","","","","destroy","","","","","","","","","","","featureP:research,true","","",0,0,"RESEARCH","","","",0,0,0,0,0,"RESEARCH",0,0,0),
					new Building("nbridge","Bridge","851",0,0,3,"north","","","research:nbridge","disabled","explo",0,0,"bridge",30,5,1,5,"","stone:6","plank:1","brick:4","tool:2","","","","","","","","","","","","destroy","","","","","","","","","","","","","",0,0,"","river","","",0,0,0,0,0,"",0,5,0),
					new Building("ntemple2","Bigger circle","addon:ncircle2",1,1,4,"north","townNear","field:building,ntemple","research:ntemple2","","needs",0,0,"",40,5,1,7,"worker:2","","plank:4","brick:10","tool:3","","","","","","","","","","","","destroy","","","","","culture:10","","","","faith:30","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("ntemple","Circle","addon:ncircle",1,1,2,"north","townNear","","research:ntemple","","needs",0,0,"",20,5,1,3,"wood:4","stone:11","worker:1","","tool:1","","","","","","","","","","","","destroy","buildUpgrade:ntemple2","","","","culture:5","","","","faith:15","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("ncircus","Circus","addon:ncircus",1,1,4,"north","townNear","","research:ncircus","","needs",0,0,"",40,5,1,5,"wood:16","worker:2","plank:8","brick:2","tool:4","","","","","","","food:-1","","","","","destroy","","","","","culture:25","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nwoodtower","Wood tower","north-tower:nwoodtower",1,1,2,"north","","","research:nwoodtower","","explo",1,0,"",20,5,5,3,"wood:12","","plank:1","","","","","","","","","","","","","","destroy","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nhouse2","House","north-more:nhouse",1,1,2,"north","townNear","field:building,nhouse","research:nhouse2","","needs",0,0,"",20,5,1,7,"wood:4","stone:15","plank:3","","tool:2","","","","","","","","","","","","destroy","","","","","worker:15","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("nhouse","Tent","north-more:ntent",1,1,1,"north","townNear","","","","needs",0,0,"",2,5,1,2,"wood:1","","","","","","","","","","","","","","","","destroy","buildUpgrade:nhouse2","","","","worker:6","","","","","","","","",0,0,"","","5-nhouse-false","",0,0,0,0,0,"",0,0,0),
					new Building("rrope","Rope Factory","819",0,0,2,"ranger","townNear","","research:rrope","disabled","prod",0,0,"",0,5,1,5,"wood:4","","leaf:4","","","worker:2","","resMin:leaf,4","","","","leaf:-4","rope:1","","","","","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rleaf","Leaf collector","194",0,0,1,"ranger","terrain:forest","","research:rleaf","disabled","prod",0,0,"",0,5,1,2,"wood:3","food:8","","","","worker:1","season:not-winter","","","","","leaf:3","","","","","","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rsling","Slingshot workshop","1114",0,0,3,"ranger","townNear","","research:rsling","disabled","prod",0,0,"",0,5,1,9,"wood:5","","leaf:6","plank:4","rope:4","worker:2","","resMin:plank,1","resMin:rope,2","","","plank:-1","rope:-2","slingshot:1","","","","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rplank","Plank creater","743",0,0,2,"ranger","townNear","","research:rplank","disabled","prod",0,0,"",0,5,1,6,"wood:11","","leaf:7","","rope:2","worker:1","","resMin:wood,2","","","","wood:-2","plank:1","","","","","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rwood","Wood collector","1838",0,0,1,"ranger","terrain:forest","","research:rwood","disabled","prod",0,0,"",0,5,1,3,"","food:4","","","","worker:1","season:not-spring","","","","","wood:3","","","","","","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rberry","Berry finder","132",0,0,1,"ranger","terrain:grass","","research:rberry","disabled","food",0,0,"",0,5,1,2,"wood:4","","leaf:3","","","worker:1","season:not-winter","","","","","food:2","","","","","","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rwapple","Winter apple","958",0,0,2,"ranger","terrain:grass","","research:rwapple","disabled","food",0,0,"",0,5,1,3,"wood:1","","leaf:4","","rope:1","","season:winter","","","","","food:6","","","","","","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rhouse","Tree house","1316",0,0,1,"ranger","terrain:forest","","research:rhouse","disabled","needs",0,0,"",0,5,1,3,"wood:5","","leaf:7","","","","","","","","","","","","","","","","","","","worker:6","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rresearch","Mediation","1900",0,0,1,"ranger","townNear","","research:rresearch","disabled","general",0,0,"",0,5,1,5,"wood:5","","","","gold:2","worker:1","","","","","","research:1","","","","","","","","","","","","","","","","","","",0,0,"RESEARCH","","","",0,0,0,0,0,"RESEARCH",0,0,0),
					new Building("rtrain","Training","1871",0,0,3,"ranger","townNear","","research:rtrain","disabled","war",0,0,"",0,5,1,7,"wood:2","slingshot:1","leaf:5","plank:3","rope:3","worker:2","","","","","","","","","","","train","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rpalast5","Biggest palast","66",0,0,4,"ranger","","field:building,rpalast4","research:rpalast5","disabled","general",0,0,"",0,5,10,28,"","","","","","","","","","","","worker:1","","","","","train:rworker,rexplorer","","trade","","","","","","","","","","","",1,0,"","","","",40,40,400,0,1,"",0,0,0),
					new Building("rpalast4","Bigger palast","34",0,0,3,"ranger","","field:building,rpalast3","research:rpalast4","disabled","general",0,0,"",0,5,8,10,"","","","","","","","","","","","worker:1","","","","","train:rworker,rexplorer","buildUpgrade:rpalast5","trade","","","","","","","","","","","",1,0,"","","","",20,40,300,0,1,"",0,0,0),
					new Building("rpalast3","Big palast","34",0,0,2,"ranger","","field:building,rpalast2","research:rpalast3","disabled","general",0,0,"",0,5,6,4,"wood:4","","","","","","","","","","","worker:1","","","","","train:rworker,rexplorer","buildUpgrade:rpalast4","trade","","","","","","","","","","","",1,0,"","","","",20,30,200,0,1,"",0,0,0),
					new Building("rpalast2","Medium palast","959",0,0,1,"ranger","","field:building,rpalast1","research:rpalast2","disabled","general",0,0,"",0,5,4,2,"wood:4","","leaf:4","","","","","","","","","worker:1","","","","","train:rworker,rexplorer","buildUpgrade:rpalast3","trade","","","","","","","","","","","",1,0,"","","","",10,20,200,0,1,"",0,0,0),
					new Building("rpalast","Small palast","959",0,0,1,"ranger","","","research:rpalast","disabled","general",0,0,"",0,5,2,1,"","","","","","","","","","","","worker:1","","","","","train:rworker,rexplorer","buildUpgrade:rpalast2","trade","","","worker:5","food:20","wood:10","leaf:5","","","","","",1,0,"","","","",10,20,300,0,0,"",0,0,0),
					new Building("ralchemy","Alchemy","1462",0,0,4,"ranger","townNear","","research:ralchemy","disabled","prod",0,0,"",0,5,1,5,"wood:6","leaf:15","plank:6","rope:4","gold:2","worker:2","","","","","","","","","","","","","","","","","","","","","","","","plank-4",0,0,"","","","",0,0,0,0,0,"",0,0,0),
					new Building("rstreet","Way","1654",0,0,2,"ranger","townNear","","research:rstreet","disabled","explo",0,0,"",0,5,0,5,"wood:1","","leaf:4","","","","","","","","","","","","","","","","","","","","","","","","","","","",0,0,"","","","",0,0,0,0,0,"",0,0,0)
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
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,57);
		public readonly string[] labels = new string[]{"id","file"};
		private Icons[] _rows = new Icons[5];
		public void Init() {
			_rows = new Icons[]{
					new Icons("logo","Icons/logo"),
					new Icons("research","Icons/magic:research"),
					new Icons("quest","Icons/base:quest"),
					new Icons("move","Icons/base:move"),
					new Icons("foundTown","Icons/base:foundTown")
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
		public System.DateTime updated = new System.DateTime(2019,11,9,23,17,57);
		public readonly string[] labels = new string[]{"Units are, besides buildings, essential to the game. When clicked, they can be moved with the arrow keys, which costs action points (AP). You can also perform various actions, which costs AP. At the end of each round, the AP will be refilled.","name","icon","text"};
		private Help[] _rows = new Help[10];
		public void Init() {
			_rows = new Help[]{
					new Help("moveUnit","Move the units","base:move","At the moment, you can only move the units with the arrow keys, if they have enough AP. Every Round the AP will be refreshed."),
					new Help("moveCamera","Move the camera","base:map","To move the camera, please use WASD on the keyboard."),
					new Help("general","General","logo","Hello! \nNice to meet you, i'm Sven and develop 9 Nations. It is a tuned-based strategy game. You have to explore the area and develop your kingdoms.\n"),
					new Help("beta","Warning","ui:debug","Hello,\nThis version of the game does not contains all features at the moment. This means that you can encounter bugs, crashes, incomplete and unpolished features, etc. Also except balancing issuses.\nPlease also \nIf you have found any mistakes, bugs oder you have questions, please use the feedback button. I'm happy to hear from you. \nGreetings Sven"),
					new Help("privacy","Privacy Policy","magic:privacy","Please take a look at the website https://9nations.de/privacy-policy/privacy-policy-in-game/"),
					new Help("new","Whats new?","base:new","Hello,\nto understand 9 nations, play the tutorial. You can find it in the campaign menu.\nYou can find the new features on the website https://9nations.de"),
					new Help("research","Research","magic:research","Since you never know what you will discover during research, research at 9N also functions somewhat differently. You choose different areas or a combination of them and start researching. The more areas you combine, the longer the research will take, but the greater the chance of researching something. For example, if I only do research in the area of life, you will discover the larger village hall. If you combine life and wood, you can also discover the larger house."),
					new Help("unit","Units","base:train","Units are, besides buildings, essential to the game. When clicked, they can be moved with the arrow keys, which costs action points (AP). You can also perform various actions, which costs AP. At the end of each round, the AP will be refilled.\nUnits are always supplied by the nearest city, if available, i.e. they use the resources of this city."),
					new Help("building","Building","base:build","Buildings are, besides the units, basic to the game. If they are clicked, actions can be triggered, which costs action points (AP). At the end of each round, the AP is replenished.\nBuildings are always part of a city, the closest city is automatically selected during construction. "),
					new Help("town","Town","base:foundTown","A city is a collection of buildings, with shared resources. A city also manages all units that are in its area.\nCities always have a center, which is represented by the town hall. The city can be levelled if the town hall is upgraded.  ")
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

	}
}