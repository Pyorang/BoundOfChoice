public class RerollTicket : ItemBase
{
    public override bool ApplyEffect()
    {
        ChoiceManager.Instance.GetNewChoice();
        return true;
    }
}
