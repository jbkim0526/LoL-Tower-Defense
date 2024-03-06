
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color cantbuildcolor;
    private Renderer rend;
    private Color startColor;
    public Vector3 positionOffset;
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private AudioSource source;
    private float vol = 0.6f;


    BuildManager buildManager;



    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        source = GetComponent<AudioSource>();    //Audio Source

        buildManager = BuildManager.instance;        // instance를 사용 
    }


    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }


    private void OnMouseDown() // 마우스 버튼을 누르면 
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;


        if (turret != null) {
            buildManager.SelectNode(this);
        }


        if (!buildManager.CanBuild)
        {
            return;
        }
        
        BuildTurret(buildManager.GetTurretToBuild());

    }

    void BuildTurret(TurretBlueprint blueprint)
    {

        if (PlayerStats.Money < blueprint.cost)
        {
            return;
        }
        PlayerStats.Money -= blueprint.cost;
        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;
        if(turret.tag == "Ezreal")
        {
            Ezreal nowEzreal = turret.transform.GetComponent<Ezreal>();
            source.PlayOneShot(nowEzreal.pickSound, vol);
        }

        if (turret.tag == "Lux")
        {
            Lux nowEzreal = turret.transform.GetComponent<Lux>();
            source.PlayOneShot(nowEzreal.pickSound, vol);
        }

        if (turret.tag == "Ziggs")
        {
            Ziggs nowEzreal = turret.transform.GetComponent<Ziggs>();
            source.PlayOneShot(nowEzreal.pickSound, vol);
        }

        if (turret.tag == "Akali")
        {
            AkaliAttack nowEzreal = turret.transform.GetComponent<AkaliAttack>();
            source.PlayOneShot(nowEzreal.pickSound, vol);
        }

        if (turret.tag == "Ashe")
        {
            Turret_Ashe nowEzreal = turret.transform.GetComponent<Turret_Ashe>();
            source.PlayOneShot(nowEzreal.pickSound, vol);
        }

        if (turret.tag == "Fizz")
        {
            Turret_Fizz nowEzreal = turret.transform.GetComponent<Turret_Fizz>();
            source.PlayOneShot(nowEzreal.pickSound, vol);
        }

        if (turret.tag == "Karthus")
        {
            Turret_Karthus nowEzreal = turret.transform.GetComponent<Turret_Karthus>();
            source.PlayOneShot(nowEzreal.pickSound, vol);
        }

        if (turret.tag == "Morgana")
        {
            Turret_Morgana nowEzreal = turret.transform.GetComponent<Turret_Morgana>();
            source.PlayOneShot(nowEzreal.pickSound, vol);
        }

        if (turret.tag == "Anivia")
        {
            Turret_Anivia nowEzreal = turret.transform.GetComponent<Turret_Anivia>();
            source.PlayOneShot(nowEzreal.pickSound, vol);
        }



        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradedCost)
        {
            return;
        }
        PlayerStats.Money -= turretBlueprint.upgradedCost;
        Destroy(turret);

        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;


        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        isUpgraded = true;

    }

    public void SellTurret()
    {

        PlayerStats.Money += turretBlueprint.GetSellAmount();
        Destroy(turret);
        turretBlueprint = null;

    }


    private void OnMouseEnter()   // 마우스를 위에 갖다대면 한번 실행 
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;


        if (buildManager.HasMoney) {
            rend.material.color = hoverColor;

        }
        else
        {

            rend.material.color = cantbuildcolor;
        }

        
        
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}
