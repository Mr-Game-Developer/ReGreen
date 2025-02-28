using DigitalRuby.RainMaker;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentalChanges : MonoBehaviour
{
    [Range(5, 70)][SerializeField] public int currentHumidity = 20; // Current Humidity
    [SerializeField] private int maxHumidity = 70; // Maximum humidity level
    [SerializeField] private int minHumidity = 5; // Minimum humidity level
    [Range(5f, 60f)][SerializeField] public float currentTemperature = 28f; // Current Temperature
    [SerializeField] private float maxTemperature = 60f; // Maximum Temperature level
    [SerializeField] private float minTemperature = 5f; // Minimum Temperature level
    [Range(0, 1)][SerializeField] private float Oxygen; // Oxygen Level in Air (0-1)
    [Range(0, 1)][SerializeField] private float CarbonDioxide; // Carbon Level in Air (0-1)
    [Range(0, 1)][SerializeField] private float Methane; // Methane Gas Level in Air (0-1)
    [Range(0, 1)][SerializeField] private float NitrogenOxide; // Nitrogen Gas Level in Air (0-1)
    [Range(0, 1)][SerializeField] private float airPollution; // Initial air pollution level (0-1)
    [Range(0, 1)][SerializeField] private float globalWarming; // Initial global warming level (0-1)

    [SerializeField] private float lightIntensityMultiplier = 1f; // Multiplier for sun intensity (adjust for desired effect)
    [SerializeField] private float maxLightIntensity = 0.5f; // Maximum sun intensity
    [SerializeField] private float minLightIntensity = 0.1f; // Minimum sun intensity
    [SerializeField] private float fogDensityMultiplier = 1f; // Multiplier for fog density (adjust for desired effect)
    [SerializeField] private float maxFogDensity = 0.04f; // Maximum fog density
    [SerializeField] private float minFogDensity = 0.001f; // Minimum fog density

    public DataContainer gdata;
    [SerializeField] public Text health;

    [SerializeField] private Image O2; // UI Image for Oxygen
    [SerializeField] private Image CO2; // UI Image for Carbon
    [SerializeField] private Image CH4; // UI Image for Methane
    [SerializeField] private Image NO2; // UI Image for Nitrogen Oxide
    [SerializeField] private Image Air; // UI Image for air pollution
    
    int ptrees, ctrees;
    [SerializeField] private RainScript rain;
    private GeneratorScript generator;
    private PauseUI_to_UIScene victoryOrLoss;

    [SerializeField] public Text centigrade; // UI Text for Temperature
    [SerializeField] public Text humidity; // UI Text for Humidity

    private void Start()
    {
        centigrade.text = currentTemperature.ToString();
        humidity.text = currentHumidity.ToString();
        rain = FindAnyObjectByType<RainScript>();
        generator = FindAnyObjectByType<GeneratorScript>();
        victoryOrLoss = FindAnyObjectByType<PauseUI_to_UIScene>();
    }

    IEnumerator HealthLoss()
    {
        yield return new WaitForSeconds(20f);
        if (currentTemperature > 50f)
        {
            gdata.gamedata.PlayerHealth -= 1;
            health.text = gdata.gamedata.PlayerHealth.ToString();
        }
    }

    void Update()
    {
        HealthLoss();
        AirPollution();
    }

    void AirPollution()
    {
        // Air Pollution
        if (Air.fillAmount != 1 && !generator.GeneratorSound.mute)
        {
            Air.fillAmount += airPollution/200;
        }

        health.text = gdata.gamedata.PlayerHealth.ToString();
    }

    void RainManaging()
    {
        // Manage Raining in Environment
        if (PlayerPrefs.GetInt("CutTree") == 1 && PlayerPrefs.GetInt("NoOfCuttedTrees") > 10)
        {
            rain.NotRaining();
        }
        else if (PlayerPrefs.GetInt("PlantTree") == 1 && PlayerPrefs.GetInt("NoOfPlantedTrees") > 10)
        {
            rain.IsRaining();
        }

        if (PlayerPrefs.GetInt("NoOfPlantedTrees") > 50)
        {
            rain.EnableWind = true;
        }
        else if (PlayerPrefs.GetInt("NoOfPlantedTrees") <= 50)
        {
            rain.EnableWind = false;
        }
    }

    // Increase When Cutting Trees
    public void IncreaseHumidityAndTemperature() // Call this function from your humidity logic
    {
        // Ensure currentHumidity doesn't go above maxHumidity
        currentHumidity = Mathf.Clamp(currentHumidity + Random.Range(0, 5), minHumidity, maxHumidity);
        currentTemperature = Mathf.Clamp(currentTemperature + Random.Range(0.3f, 1f), minTemperature, maxTemperature);

        RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, maxFogDensity, fogDensityMultiplier * Time.deltaTime); // Gradually increase fog density
        humidity.text = currentHumidity.ToString();
        centigrade.text = currentTemperature.ToString();
    }

    // Decrease When Planting Trees
    public void DecreaseHumidityAndTemperature()
    {
        // Ensure currentHumidity doesn't go below minHumidity
        currentHumidity = Mathf.Clamp(currentHumidity - Random.Range(0, 5), minHumidity, maxHumidity);
        currentTemperature = Mathf.Clamp(currentTemperature - Random.Range(0.3f, 1f), minTemperature, maxTemperature);

        RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, minFogDensity, fogDensityMultiplier * Time.deltaTime);
        centigrade.text = currentTemperature.ToString();
        humidity.text = currentHumidity.ToString();
    }

    void UpdateUI()
    {
        // Oxygen
        if(O2.fillAmount != 0 && PlayerPrefs.GetInt("CutTree") == 1)
        {
            O2.fillAmount -= Oxygen;
        }
        else if(O2.fillAmount != 1 && PlayerPrefs.GetInt("PlantTree") == 1)
        {
            O2.fillAmount += Oxygen;
        }
        
        // Carbon Dioxide
        if(CO2.fillAmount != 1 && PlayerPrefs.GetInt("CutTree") == 1)
        {
            CO2.fillAmount += CarbonDioxide;
        }
        else if(CO2.fillAmount != 0 && PlayerPrefs.GetInt("PlantTree") == 1)
        {
            CO2.fillAmount -= CarbonDioxide;
        }
        
        // Nitrogen Oxide
        if(NO2.fillAmount != 1 && PlayerPrefs.GetInt("CutTree") == 1)
        {
            NO2.fillAmount += NitrogenOxide;
        }
        else if(NO2.fillAmount != 0 && PlayerPrefs.GetInt("PlantTree") == 1)
        {
            NO2.fillAmount -= NitrogenOxide;
        }
        
        // Methane
        if(CH4.fillAmount != 1 && PlayerPrefs.GetInt("CutTree") == 1)
        {
            CH4.fillAmount += Methane;
        }
        else if(CH4.fillAmount != 0 && PlayerPrefs.GetInt("PlantTree") == 1)
        {
            CH4.fillAmount -= Methane;
        }
        
        // Air Pollution
        if(Air.fillAmount != 1 && PlayerPrefs.GetInt("CutTree") == 1)
        {
            Air.fillAmount += airPollution;
        }
        else if(Air.fillAmount != 0 && PlayerPrefs.GetInt("PlantTree") == 1)
        {
            Air.fillAmount -= airPollution;
        }

        if(O2.fillAmount > 0.8f && CO2.fillAmount < 0.2f && NO2.fillAmount < 0.4f && CH4.fillAmount < 0.3f && Air.fillAmount < 0.2f && currentTemperature < 30f && gdata.gamedata.PlayerHealth > 50)
        {
            victoryOrLoss.Victory();
        }
        else if (O2.fillAmount < 0.2f && CO2.fillAmount > 0.7f && NO2.fillAmount > 0.8f && CH4.fillAmount > 0.6f && Air.fillAmount > 0.7f && currentTemperature > 50f && gdata.gamedata.PlayerHealth < 40)
        {
            victoryOrLoss.Loss();
        }
    }

    public void CutTree()
    {
        ctrees++;
        PlayerPrefs.SetInt("NoOfCuttedTrees", ctrees);
        PlayerPrefs.SetInt("CutTree", 1);
        PlayerPrefs.SetInt("PlantTree", 0);

        if (RenderSettings.fogDensity !< maxFogDensity)
        {
            RenderSettings.fogDensity += minFogDensity; // Air Pollution Fog Density Adjusting
        }
        RenderSettings.ambientIntensity += 0.01f; // Light Adjusting Humadity

        // Balancing Rain base on trees
        if (ptrees > 0)
        {
            ptrees--;
            PlayerPrefs.SetInt("NoOfPlantedTrees", ptrees);
        }

        gdata.gamedata.PlayerHealth -= 1;
        health.text = gdata.gamedata.PlayerHealth.ToString();

        UpdateUI();
        RainManaging();
        IncreaseHumidityAndTemperature();
    }

    public void PlantTree()
    {
        ptrees++;
        PlayerPrefs.SetInt("NoOfPlantedTrees", ptrees);
        PlayerPrefs.SetInt("CutTree", 0);
        PlayerPrefs.SetInt("PlantTree", 1);

        if (RenderSettings.fogDensity !> minFogDensity)
        {
            RenderSettings.fogDensity -= minFogDensity; // Air Pollution Fog Density Adjusting
        }
        RenderSettings.ambientIntensity -= 0.01f; // Light Adjusting Humadity

        // Balancing Rain base on trees
        if(ctrees > 0)
        {
            ctrees--;
            PlayerPrefs.SetInt("NoOfCuttedTrees", ctrees);
        }

        gdata.gamedata.PlayerHealth += 1;
        health.text = gdata.gamedata.PlayerHealth.ToString();

        UpdateUI();
        RainManaging();
        DecreaseHumidityAndTemperature();
    }
}