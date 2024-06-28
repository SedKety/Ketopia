using UnityEngine;
[CreateAssetMenu(menuName = "Item/RecipeItem")]
public class RecipeItemScript : Item
{
    public Recipe heldRecipe;
    public CrafterType type;

    public string displayString;
    public bool shouldDisplay;
    public AudioClip clip;

    public bool shouldSpawnIsland;
    public override void OnItemUse()
    {
        RecipeManager.OnRecipeAdd(this);
        if (shouldDisplay)
        {
            UIScript.instance.DisplayText(displayString);
        }
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, PlayerManager.instance.gameObject.transform.position);
        }
        if (shouldSpawnIsland & GameManger.instance.canyonActive == false)
        {
            GameManger.instance.SpawnCanyonIsland();
            UIScript.instance.DisplayText(displayString);
        }
    }
}
