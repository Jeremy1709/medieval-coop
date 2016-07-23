using UnityEngine;
using System.Collections;

public class weaponStats : MonoBehaviour { //MELEE ONLY 

    public int iconCode;
    public string itemName;
    public string itemClass;
    public float density;
    public float hardness;
    public float sharpness;
    public float handleDensity;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float weight;

    public float bluntModifier;
    public float pierceModifier;
    public float slashModifier;
    public float weightModifier;

    

    // Use this for initialization
    void Start() {

        weightModifiers();
        //getDamage(1, "Slash");
        //getDamage(1, "Pierce");
        //getDamage(1, "Overhead");
        //Debug.Log("                                                 ^^^^ "+itemClass);
        
    }

    // Update is called once per frame
    void Update() {
        
    }
    //maybe output a 3 prong array for slash pierce and blunt damage
    
    public float[] getDamage(float hitSpeed, string attackType) //Hitspeed is just how close to the "perfect hit" spot it is
    {
        //I NEED TO ADD A LIMB INPUT TO THE DAMAGE CALULATION
        float bluntDamage = ((1 / sharpness) + density * hardness) * hitSpeed * bluntModifier + handleDensity*6 + Random.Range(-2, 2);
        float slashDamage = ((sharpness * 10f) + density * 1.5f + hardness * 2f) * hitSpeed * slashModifier + handleDensity * 4 + Random.Range(-2, 2);
        float pierceDamage = ((sharpness * 15f) + density + hardness * 2f) * hitSpeed * pierceModifier + handleDensity * 5 + Random.Range(-2, 2);

        if (attackType.Equals("Slash")) {
            bluntDamage *= 0.7f;
            slashDamage *= 1.2f;
            pierceDamage *= 0.2f;
        }
        else if (attackType.Equals("Pierce")){
            bluntDamage *= 0.2f;
            slashDamage *= 0.4f;
            pierceDamage *= 1.2f;
        }
        else if (attackType.Equals("Overhead")){
            bluntDamage *= 1.1f;
            slashDamage *= 0.7f;
            pierceDamage *= 0.3f;
        }

            Debug.Log("Blunt: " + bluntDamage + " Slash: " + slashDamage + " Pierce : " + pierceDamage +" Total Damage: "+(bluntDamage+ slashDamage+ pierceDamage)+ " Name: " +name + " Attack:" +attackType);

        float[] outputDamage = { bluntDamage, slashDamage, pierceDamage };

        return outputDamage;
    }

    public void createWeapon(int iconCode1, string itemName1, string weaponClass1, float density1, float hardness1, float sharpness1, float handleDensity1)
    {
        iconCode = iconCode1;
        itemName = itemName1;
        itemClass =  weaponClass1;
        density = density1;
        hardness =  hardness1;
        sharpness = sharpness1;
        handleDensity = handleDensity1;

        weightModifiers();
    }

    public void weightModifiers()
    {
        if (itemClass.Equals("Sword"))
        {
            bluntModifier = 0.4f;
            pierceModifier = 0.2f;
            slashModifier = 0.6f;
            weightModifier = 2.3f;
        }
        else if (itemClass.Equals("Axe"))
        {
            bluntModifier = 0.7f;
            pierceModifier = 0.1f;
            slashModifier = 0.55f;
            weightModifier = 3.5f; //should be called weight modifier i think
        }
        else if (itemClass.Equals("Mace"))
        {
            bluntModifier = 1f;
            pierceModifier = 0.1f;
            slashModifier = 0.2f;
            weightModifier = 5f;
        }
        else if (itemClass.Equals("Spear"))
        {
            bluntModifier = 0.15f;
            pierceModifier = 0.75f;
            slashModifier = 0.2f;
            weightModifier = 1.8f;
        }
        else if (itemClass.Equals("Dagger")) 
        {
            bluntModifier = 0.1f;
            pierceModifier = 0.6f;
            slashModifier = 0.25f;
            weightModifier = 1.5f;
        }
        else if (itemClass.Equals("Arrow")) //This and below not tested at all, but the above values seem decent
        {
            bluntModifier = 0.2f;
            pierceModifier = 1f;
            slashModifier = 0.15f;
        }
        else if (itemClass.Equals("Bolt"))
        {
            bluntModifier = 0.5f;
            pierceModifier = 0.8f;
            slashModifier = 0.15f;
        }
        //CALCULATING WEIGHT AND SPEED BASED ON THE CLASS AND MATERIAL DENSITY OF THE HEAD
        weight = (density * weightModifier) * (1 / 8.4f);
        speed = weight / 2.2f;
        speed = 1 / speed;
    }

    public float getSpeed()
    {
        return speed;
    }
}
