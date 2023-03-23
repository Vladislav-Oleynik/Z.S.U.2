using UnityEngine;
using System.Collections;

public class GlobalValue : MonoBehaviour {
    public static bool isFirstOpenMainMenu = true;
	public static int worldPlaying = 1;
	public static int levelPlaying = 1;
    public static int killedZombies = 0;
    public static int killedVehicles = 0;
    //public static int finishGameAtLevel = 50;

    public static string WorldReached = "WorldReached";
	public static bool isSound = true;
	public static bool isMusic = true;

    public static bool isNewGame
    {
        get { return PlayerPrefs.GetInt("isNewGame", 0) == 0; }
        set { PlayerPrefs.SetInt("isNewGame", value ? 0 : 1); }
    }

    public static bool isEarnCoin
    {
        get { return PlayerPrefs.GetInt("isEarnCoin", 0) == 1; }
        set { PlayerPrefs.SetInt("isEarnCoin", value ? 1 : 0); }
    }

    public static int lastDayShowNativeAd1{
		get { return PlayerPrefs.GetInt ("lastDayShowNativeAd1", 0); }
		set{ PlayerPrefs.SetInt ("lastDayShowNativeAd1", value); }
	}

	public static int lastDayShowNativeAd2{
		get { return PlayerPrefs.GetInt ("lastDayShowNativeAd2", 0); }
		set{ PlayerPrefs.SetInt ("lastDayShowNativeAd2", value); }
	}

    public static int GetBullet(int ID, int defaultBullet )
    {
        return PlayerPrefs.GetInt("GetBullet" + ID, defaultBullet);
    }

    public static void SetBullet(int ID, int value)
    {
        PlayerPrefs.SetInt("GetBullet" + ID, value);
    }

    public static int lastDayShowNativeAd3{
		get { return PlayerPrefs.GetInt ("lastDayShowNativeAd3", 0); }
		set{ PlayerPrefs.SetInt ("lastDayShowNativeAd3", value); }
	}

	public static int SavedCoins
    {
        get { return PlayerPrefs.GetInt("Coins", 200); }
        set
        {
            isEarnCoin = true;
            PlayerPrefs.SetInt("Coins", value);
        }
    }
    
    public static int LevelPass { 
		get { return PlayerPrefs.GetInt ("LevelReached", 0); } 
		set { PlayerPrefs.SetInt ("LevelReached", value); } 
	}

	public static void LevelStar(int level, int stars){
		PlayerPrefs.SetInt ("LevelStars" + level, stars);
	}

	public static int LevelStar(int level){
		return PlayerPrefs.GetInt ("LevelStars" + level, 0); 
	}

	public static bool RemoveAds { 
		get { return PlayerPrefs.GetInt ("RemoveAds", 0) == 1 ? true : false; } 
		set { PlayerPrefs.SetInt ("RemoveAds", value ? 1 : 0); } 
	}

    public static int ItemDoubleArrow
    {
        get { return PlayerPrefs.GetInt("ItemDoubleArrow", 3); }
        set { PlayerPrefs.SetInt("ItemDoubleArrow", value); }
    }

    public static int ItemTripleArrow
    {
        get { return PlayerPrefs.GetInt("ItemTripleArrow", 1); }
        set { PlayerPrefs.SetInt("ItemTripleArrow", value); }
    }

    public static int ItemPoison
    {
        get { return PlayerPrefs.GetInt("ItemPoison", 3); }
        set { PlayerPrefs.SetInt("ItemPoison", value); }
    }

    public static int ItemFreeze
    {
        get { return PlayerPrefs.GetInt("ItemFreeze", 3); }
        set { PlayerPrefs.SetInt("ItemFreeze", value); }
    }

    public static bool isPicked(GunTypeID gunID)
    {
       return PlayerPrefs.GetString("GUNTYPE" + gunID.gunType, "") == gunID.gunID;
    }

    public static void pickGun(GunTypeID gunID)
    {
        PlayerPrefs.SetString("GUNTYPE" + gunID.gunType, gunID.gunID);
    }

    public static void LoadPlayerLoadout(ref PlayerLoadout loadout) 
    {
        string raw_str = PlayerPrefs.GetString("LOADOUT", "0,0,0,0,0,0,0,0|DefaultHead,DefaulHead_acc,DefaultTorso,DefaultHands,DefaultLegs,DefaultFeet|Shotgun|AssaultRifle");
        Debug.Log(raw_str);
        string[] pieces = raw_str.Split(new[] { '|' });
        //string test = "1,1,1,0,1,0,0,1|DefaultHead,BlackGoggles,CamoJacket,DefaultHands,BlackPants,BlackShoes|Shotgun|AssaultRifle2";
        //string[] pieces = test.Split(new[] { '|' });
        string[] idx = pieces[0].Split(new[] { ',' });
        string[] items = pieces[1].Split(new[] { ',' });
        string[] fweapons = pieces[2].Split(new[] { ',' });
        string[] sweapons = pieces[3].Split(new[] { ',' });
        
        //foreach (var item in idx)
        //{
        //    Debug.Log(item + "\n");
        //}

        loadout.head_accIdx = int.Parse(idx[0]);
        loadout.torsoIdx = int.Parse(idx[1]);
        loadout.legsIdx = int.Parse(idx[2]);
        loadout.headIdx = int.Parse(idx[3]);
        loadout.feetIdx = int.Parse(idx[4]);
        loadout.handsIdx = int.Parse(idx[5]);
        loadout.firstWeaponIdx = int.Parse(idx[6]);
        loadout.secondWeaponIdx = int.Parse(idx[7]);

        //foreach (var item in items)
        //{
        //    Debug.Log(item + "\n");
        //}

        Debug.Log("loadout.customItems[0] = " + loadout.customItems[0]);
        Debug.Log("ItemsContainer.headsList = " + ItemsContainer.headsList.Count);
        Debug.Log("ItemsContainer.headsList.Find(x => x.GetItemName() == items[0]) = " + ItemsContainer.headsList.Find(x => x.GetItemName() == items[0]));
        loadout.customItems[0] = ItemsContainer.headsList.Find(x => x.GetItemName() == items[0]);
        loadout.customItems[1] = ItemsContainer.head_accsList.Find(x => x.GetItemName() == items[1]);
        loadout.customItems[2] = ItemsContainer.torsosList.Find(x => x.GetItemName() == items[2]);
        loadout.customItems[3] = ItemsContainer.handsList.Find(x => x.GetItemName() == items[3]);
        loadout.customItems[4] = ItemsContainer.legsList.Find(x => x.GetItemName() == items[4]);
        loadout.customItems[5] = ItemsContainer.feetList.Find(x => x.GetItemName() == items[5]);
        

        //foreach (var item in fweapons)
        //{
        //    Debug.Log(item + "\n");
        //}

        for (int i = 0; i < loadout.firstWeapons.Count; i++)
        {
            loadout.firstWeapons[i] = ItemsContainer.firstWeaponsList.Find(x => x.gunName == fweapons[i]);
        }        

        //foreach (var item in sweapons)
        //{
        //    Debug.Log(item + "\n");
        //}

        for (int i = 0; i < loadout.firstWeapons.Count; i++)
        {
            loadout.firstWeapons[i] = ItemsContainer.secondWeaponsList.Find(x => x.gunName == sweapons[i]);
        }

        //TODO: брать название каждого объекта и искать его в ItemsConteiner, проходить по каждому елементу списка и находить совпадающее название, совпавший объект заменять в loadout

    }

    public static void SaveLoadout(PlayerLoadout loadout)
    {
        string loadout_str = "";
        string loadout_idx_str = "";
        string loadout_items_str = "";
        string loadout_fweapons_str = "";
        string loadout_sweapons_str = "";

        loadout_idx_str += loadout.head_accIdx + ",";
        loadout_idx_str += loadout.torsoIdx + ",";
        loadout_idx_str += loadout.legsIdx + ",";
        loadout_idx_str += loadout.headIdx + ",";
        loadout_idx_str += loadout.feetIdx + ",";
        loadout_idx_str += loadout.handsIdx + ",";
        loadout_idx_str += loadout.firstWeaponIdx + ",";
        loadout_idx_str += loadout.secondWeaponIdx;

        for (int i = 0; i < loadout.customItems.Count; i++)
        {
            loadout_items_str += loadout.customItems[i].GetItemName();
            if(i != loadout.customItems.Count - 1) 
            {
                loadout_items_str += ",";
            }
        }

        for (int i = 0; i < loadout.firstWeapons.Count; i++)
        {
            loadout_fweapons_str += loadout.firstWeapons[i].gunName;
            if (i != loadout.firstWeapons.Count - 1)
            {
                loadout_fweapons_str += ",";
            }
        }

        for (int i = 0; i < loadout.secondWeapons.Count; i++)
        {
            loadout_sweapons_str += loadout.secondWeapons[i].gunName;
            if (i != loadout.secondWeapons.Count - 1)
            {
                loadout_sweapons_str += ",";
            }
        }

        loadout_str += loadout_idx_str + "|" + loadout_items_str + "|" + loadout_fweapons_str + "|" + loadout_sweapons_str;
        Debug.Log(loadout_str);

        PlayerPrefs.SetString("LOADOUT", loadout_str);
    }
}