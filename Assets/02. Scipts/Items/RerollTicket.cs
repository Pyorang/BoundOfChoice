public class RerollTicket : ItemBase
{
    public override void ApplyEffect()
    {
        ChoiceManager.Instance.GetNewChoice();
    }
}
