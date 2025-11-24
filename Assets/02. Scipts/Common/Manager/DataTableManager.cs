using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataTableManager : SingletonBehaviour<DataTableManager>
{
    [SerializeField]
    class Wrapper<T>
    {
        public T[] data;
    }

    private ChoiceModel[] _choiceTable;
    private ItemModel[] _itemTable;

    protected override void Init()
    {
        base.Init();

        _choiceTable = LoadDataFromJson<ChoiceModel>("Choice");
        _itemTable = LoadDataFromJson<ItemModel>("Item");
    }

    private const string DATA_PATH = "DataTable";

    private T[] LoadDataFromJson<T>(string filename)
    {
        var path = Path.Combine(DATA_PATH, filename);
        var json = Resources.Load<TextAsset>(path);
        var wrapper = JsonUtility.FromJson<Wrapper<T>>(json.text);
        return wrapper.data;
    }

    public int GetChoiceCount()
    {
        return _choiceTable.Length;
    }

    public ChoiceModel GetChoice(int id)
    {
        return _choiceTable.FirstOrDefault(x => x.ID == id.ToString());
    }

    public ChoiceModel GetRandomChoice(params int[] exclusives)
    {
        var exclusiveIds = new HashSet<string>(
            exclusives.Select(id => $"C_{id}")
        );

        var availableChoices = _choiceTable.Where(x => !exclusiveIds.Contains(x.ID));

        var availableChoicesArray = availableChoices.ToArray();
        if (availableChoicesArray.Length == 0)
        {
            return default;
        }

        return availableChoicesArray[UnityEngine.Random.Range(0, availableChoicesArray.Length)];
    }

    public ItemModel GetItemModel(int index)
    {
        return _itemTable[index];
    }

    public string GetItemName(string itemId)
    {
        ItemModel item = _itemTable.FirstOrDefault(x => x.ID == itemId);
        return item.Name;
    }

    public string GetItemDescription(string itemId)
    {
        ItemModel item = _itemTable.FirstOrDefault(x => x.ID == itemId);
        return item.Description;
    }
}
