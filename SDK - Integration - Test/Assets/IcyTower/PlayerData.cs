using Microsoft.Win32;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "IcyTower/Player Data")]
public class PlayerData : ScriptableObject
{

    /// <summary>
    /// LEADERBOARD DATAS
    /// </summary>

    [SerializeField]
    private int _maxCombo = 0;

    [SerializeField]
    private int _maxScore = 0;

    [SerializeField]
    private int _maxFloor = 0;

    /// <summary>
    /// VIRTUAL CURRENCIES
    /// </summary>

    [SerializeField]
    private int _goldAmount = 0;

    [SerializeField]
    private int _gemAmount = 0;

    [SerializeField]
    private int _diamoundAmount = 0;

    /// <summary>
    /// PLAYER INVENTORY
    /// </summary>

    /// < ClothesID, isEquipped >
    /// ClothesID is an unique item ID.
    /// isEquipped, defines whether avatar is equipped this item or not
    [SerializeField]
    private Dictionary<string, bool> _clothesList;

    /// < PowerupID, maxLevel >
    /// PowerupID is an unique item ID.
    /// maxLevel, defines whether the item is upgradeble or not.
    /// Also if it's upgradebla, maxLevel defines max ugrade level of powerup.
    [SerializeField]
    private Dictionary<string, int> _powerUpList;

    [SerializeField]
    private List<string> _keyList;

    /// <summary>
    /// Player can store special items in inventory but max count of this item is limited by two.
    /// </summary>
    [SerializeField]
    private List<string> _specialItemList;

    [SerializeField]
    private List<string> _permanentItemList;


    #region Getter - Setter

    public int MaxCombo
    {
        get { return _maxCombo; }
        set { _maxCombo = value; }
    }

    public int MaxScore
    {
        get { return _maxScore; }
        set { _maxScore = value; }
    }

    public int MaxFloor
    {
        get { return _maxFloor; }
        set { _maxFloor = value; }
    }

    public int GemAmount
    {
        get { return _gemAmount; }
        set { _gemAmount = value; }
    }

    public int GoldAmount
    {
        get { return _goldAmount; }
        set { _goldAmount = value; }
    }

    public int DiamondAmount
    {
        get { return _diamoundAmount; }
        set { _diamoundAmount = value; }
    }

    public Dictionary <string, bool> ClothesList
    {
        get { return _clothesList; }
        set { _clothesList = value; }
    }

    public Dictionary <string, int> PowerUpList
    {
        get { return _powerUpList; }
        set { _powerUpList = value; }
    }

    public List <string> KeyList
    {
        get { return _keyList; }
        set { _keyList = value; }
    }

    public List <string> SpecialItemList
    {
        get { return _specialItemList; }
        set { 

            if(value.Count <= 2)
            {
                _specialItemList = value;
            }
           
        }

    }

    public List<string> PermanentItemList
    {
        get { return _permanentItemList; }
        set { _permanentItemList = value; }
    }

    #endregion

}
