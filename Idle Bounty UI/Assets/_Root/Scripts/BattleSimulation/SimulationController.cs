using UnityEngine;

public class SimulationController : MonoBehaviour
{
    public BattleUnit HeroUnit;
    public BattleUnit Enemy;

    void Start()
    {
        SpawnUnits();   
    }

    void SpawnUnits()
    {
        var _HeroUnit = Instantiate(HeroUnit);
        var _Enemy = Instantiate(Enemy);

        _HeroUnit.InitializeUnit(_Enemy);
        _Enemy.InitializeUnit(_HeroUnit);
    }
}
