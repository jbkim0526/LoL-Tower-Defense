
using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour
{

    public TurretBlueprint Lux;
    public TurretBlueprint Morgana;
    public TurretBlueprint Ezreal;
    public TurretBlueprint Ziggs;
    public TurretBlueprint Akali;
    public TurretBlueprint Karthus;
    public TurretBlueprint Anivia;
    public TurretBlueprint Fizz;
    public TurretBlueprint Ashe;
    

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }



    public void SelectLux()
    {

        buildManager.SelectTurretToBuild(Lux);

    }

    public void SelectMorgana()
    {

        buildManager.SelectTurretToBuild(Morgana);

    }

    public void SelectKarthus()
    {

        buildManager.SelectTurretToBuild(Karthus);

    }

    public void SelectAnivia()
    {

        buildManager.SelectTurretToBuild(Anivia);

    }

    public void SelectFizz()
    {

        buildManager.SelectTurretToBuild(Fizz);

    }

    public void SelectAshe()
    {

        buildManager.SelectTurretToBuild(Ashe);

    }

    public void SelectEzreal()
    {

        buildManager.SelectTurretToBuild(Ezreal);

    }


    public void SelectZiggs()
    {

        buildManager.SelectTurretToBuild(Ziggs);

    }

    public void SelectAkali()
    {
        buildManager.SelectTurretToBuild(Akali);
    }

  
}
