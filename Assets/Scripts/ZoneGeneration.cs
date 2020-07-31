using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGeneration : MonoBehaviour
{
    public byte [,] zoneSurface;
    int surfaceWidthSlots;
    int surfaceDepthSlots;
    float baseWidth;
    float baseDepth;
    float blockWidth;
    float blockDepth;
    float blockHeight;
    GameObject ground;
    public int wallCount;
    public GameObject wallPrefab;
    public int propCount;
    public GameObject propPrefab;
    public int groundHazardCount;
    public GameObject groundHazardPrefab;
    public int drCapCount;
    public GameObject drCapPrefab;
    public int virusCount;
    public GameObject[] virusPrefab;

    void Start()
    {
        ground = transform.Find(gameObject.name + " Ground").gameObject;
        baseWidth = ground.GetComponent<Renderer>().bounds.size.x;
        baseDepth = ground.GetComponent<Renderer>().bounds.size.z;
        blockWidth = wallPrefab.GetComponent<Renderer>().bounds.size.x;
        blockDepth = wallPrefab.GetComponent<Renderer>().bounds.size.z;
        blockHeight = wallPrefab.GetComponent<Renderer>().bounds.size.y;
        // La proporción entre el tamaño de la base y el tamaño de los bloques se traduce en una retícula de
        // tamaño entero que se lleva a un array bidimensional que contendrá un valor según el objeto único
        // de obstáculo, enemigo o moneda que se vaya a instanciar.
        surfaceWidthSlots = (int)Mathf.Floor(baseWidth / blockWidth);
        surfaceDepthSlots = (int)Mathf.Floor(baseWidth / blockWidth);
        zoneSurface = new byte[surfaceWidthSlots, surfaceDepthSlots];

        if (GameState.newGame == true) GenerateGrid();
        else
        {
            switch (gameObject.name)
            {
                case "Lab":
                    zoneSurface = GameState.Lab;
                    break;
                case "Desert":
                    zoneSurface = GameState.Desert;
                    break;
                case "Beach":
                    zoneSurface = GameState.Beach;
                    break;
                case "Plains":
                    zoneSurface = GameState.Plains;
                    break;
                default:
                    break;
            }
        }

        DeployGrid();
    }
    void GenerateGrid()
    {
        int xPos;
        int yPos;
        // vamos a dejar despejadas las cuatro esquinas para que no se instancie nada sobre el spawn point o el
        // teletransportador y para que las posibilidades de que haya un camino cerrado entre un punto y otro sean mínimas
        for (xPos = 0; xPos < surfaceWidthSlots; xPos++)
        {
            for (yPos = 0; yPos < surfaceDepthSlots; yPos++)
            {
                if ((yPos < 2 || yPos > (surfaceDepthSlots - 3)) && (xPos < 2 || xPos > (surfaceWidthSlots - 3))) zoneSurface[xPos, yPos] = 9;
            }
        }

        while (wallCount > 0)
        {
            xPos = Random.Range(0, surfaceWidthSlots);
            yPos = Random.Range(0, surfaceDepthSlots);
            if (zoneSurface [xPos, yPos] == 0)
            {
                zoneSurface[xPos, yPos] = 1;
                wallCount--;
            }
        }
        while (propCount > 0)
        {
            xPos = Random.Range(0, surfaceWidthSlots);
            yPos = Random.Range(0, surfaceDepthSlots);
            if (zoneSurface[xPos, yPos] == 0)
            {
                zoneSurface[xPos, yPos] = 2;
                propCount--;
            }
        }
        while (groundHazardCount > 0)
        {
            xPos = Random.Range(0, surfaceWidthSlots);
            yPos = Random.Range(0, surfaceDepthSlots);
            if (zoneSurface[xPos, yPos] == 0)
            {
                zoneSurface[xPos, yPos] = 3;
                groundHazardCount--;
            }
        }
        while (drCapCount > 0)
        {
            xPos = Random.Range(0, surfaceWidthSlots);
            yPos = Random.Range(0, surfaceDepthSlots);
            if (zoneSurface[xPos, yPos] == 0)
            {
                zoneSurface[xPos, yPos] = 4;
                drCapCount--;
            }
        }
        while (virusCount > 0)
        {
            xPos = Random.Range(0, surfaceWidthSlots);
            yPos = Random.Range(0, surfaceDepthSlots);
            if (zoneSurface[xPos, yPos] == 0)
            {
                zoneSurface[xPos, yPos] = 5;
                virusCount--;
            }
        }

        switch (gameObject.name)
        {
            case "Lab":
                GameState.Lab = zoneSurface;
                break;
            case "Desert":
                GameState.Desert = zoneSurface;
                break;
            case "Beach":
                GameState.Beach = zoneSurface;
                break;
            case "Plains":
                GameState.Plains = zoneSurface;
                break;
            default:
                break;
        }
    }
    void DeployGrid()
    {
        int xPos;
        int yPos;
        for (xPos = 0; xPos < surfaceWidthSlots; xPos++)
        {
            for (yPos = 0; yPos < surfaceDepthSlots; yPos++)
            {
                if (zoneSurface[xPos, yPos] == 1)
                {
                    Instantiate(wallPrefab, ground.transform.position + new Vector3(-(baseWidth / 2) + (blockWidth / 2) + (xPos * blockWidth), blockHeight / 2, -(baseDepth / 2) + (blockDepth / 2) + (yPos * blockDepth)), Quaternion.identity, transform);
                }
                else if (zoneSurface[xPos, yPos] == 2)
                {
                    Instantiate(propPrefab, ground.transform.position + new Vector3(-(baseWidth / 2) + (blockWidth / 2) + (xPos * blockWidth), 0 , -(baseDepth / 2) + (blockDepth / 2) + (yPos * blockDepth)), Quaternion.identity, transform);
                }
                else if (zoneSurface[xPos, yPos] == 3)
                {
                    Instantiate(groundHazardPrefab, ground.transform.position + new Vector3(-(baseWidth / 2) + (blockWidth / 2) + (xPos * blockWidth), 0.05f, -(baseDepth / 2) + (blockDepth / 2) + (yPos * blockDepth)), Quaternion.identity, transform);
                }
                else if (zoneSurface[xPos, yPos] == 4)
                {
                    GameObject newCap = Instantiate(drCapPrefab, ground.transform.position + new Vector3(-(baseWidth / 2) + (blockWidth / 2) + (xPos * blockWidth), 1.5f, -(baseDepth / 2) + (blockDepth / 2) + (yPos * blockDepth)), Quaternion.Euler(90, 0, 0), transform);
                    // Las cápsulas sirven para crear la lista de waypoints para los virus, hay que etiquetarlas según la zona navegable en la que aparezcan
                    newCap.tag = "DrCap" + gameObject.name;
                    // Las cápsulas desaparecen al cogerlas, hay que controlar en qué posición estaban para borrarlas de la cuadrícula y que no reaparezcan cuando se carga la partida
                    newCap.GetComponent<DrCapMng>().gridPosX = xPos;
                    newCap.GetComponent<DrCapMng>().gridPosY = yPos;
                }
                else if (zoneSurface[xPos, yPos] == 5)
                {
                    // El prefab de personaje virus se elige al azar de dentro de un array de tres posibles
                    GameObject newVirus = Instantiate(virusPrefab[Random.Range(0, 3)], ground.transform.position + new Vector3(-(baseWidth / 2) + (blockWidth / 2) + (xPos * blockWidth), 1.5f, -(baseDepth / 2) + (blockDepth / 2) + (yPos * blockDepth)), Quaternion.identity, transform);
                    newVirus.GetComponent<PatrolManage>().zone = gameObject.name;
                }
            }
        }
        
    }
}
