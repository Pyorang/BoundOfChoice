using System;

#region Choice
[Serializable] public struct ChoiceModel
{
    public string ID;
    public string Text1;
    public string Text2;
}
#endregion

#region Item
[Serializable] public struct ItemModel
{
    public string ID;
    public int Price;
    public string Name;
    public string Description;
}
#endregion