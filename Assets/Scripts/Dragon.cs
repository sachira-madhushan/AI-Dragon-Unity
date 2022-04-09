using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Dragon : MonoBehaviour
{


    

    float minValue;
    List<float> distance = new List<float>();
    bool wantToCheck = true;
    int indexOfNearestObject;
    GameObject nearestTower;
    GameObject[] flag;
    int nowSecond=0,calcTime=0,counter=0;
    public Text subtitles;





    void Start()
    {

        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");//all towers
        flag = GameObject.FindGameObjectsWithTag("Start");//Starting point(flag)
        int index = checkNearestTower(towers, wantToCheck,flag[0]);//get nearest tower index
        moveStart(flag[0]);
        


    }

    void moveStart(GameObject flag)
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        int index = checkNearestTower(towers, wantToCheck, flag);
        counter += 1;

        if (counter < 5)
        {
            nearestTower = towers[index];

        }
        else
        {
            wantToCheck = false;
        }
       
        



    }

    void FixedUpdate()
    {

        nowSecond += 1;
        if (wantToCheck != false)
        {
            if (nowSecond < calcTime + 200)
            {
                MoveTowoardNearestTower(nearestTower);
                subtitles.text= "Now Nearest Tower Is "+nearestTower.name+" One.\n\t\t Let's Move And Rotate";
            }
            else
            {
                moveStart(flag[0]);
                nowSecond = 0;
            }

        }
        else
        {
            comeBack(flag[0]);
        }


      
       
    

    }

    //Check which tower is nearest
    int checkNearestTower(GameObject[] towers, bool wantToCheck,GameObject flag)
    {




        if (towers.Length != 0)
        {
            if (wantToCheck == true)
            {

                for (int x = (int)towers.Length; x > 0; x--)
                {
                    float dis = Vector3.Distance(towers[x - 1].transform.position, transform.position);

                    distance.Add(dis);

                }

                minValue = distance[towers.Length - 1];

                for (int c = (int)towers.Length - 1; c > 0; c--)
                {



                    minValue = System.Math.Min(minValue, distance[c - 1]);

                }

                if (towers.Length > 2)
                {

                    indexOfNearestObject = towers.Length - (distance.IndexOf(minValue) + 1);

                    
                }
                else
                {
                    indexOfNearestObject = distance.IndexOf(minValue);
                }
                
                


            }
            else
            {
         
                comeBack(flag);

            }

        }
        else
        {
        
            comeBack(flag);
        }
       

        return indexOfNearestObject;

    }//checkNearestTower





    //go To woard the nearest Tower
    
    void MoveTowoardNearestTower(GameObject game)
    {
        
        float distance = Vector3.Distance(game.transform.position, transform.position);
        transform.forward= Vector3.RotateTowards(transform.forward, game.transform.position, 10* Time.deltaTime, 1.0f);
        if (distance > 10)
        {

            transform.position = Vector3.MoveTowards(transform.position, game.transform.position, 10 * Time.deltaTime);
            calcTime += 1;
        }

        else
        {
            rotateAround(game);

        }
        
    }//MoveTowoardNearestTower



    //rotate around the tower
    void rotateAround(GameObject n)
    {
        transform.RotateAround(n.transform.position, Vector3.up, 100* Time.deltaTime);

        if (nowSecond > calcTime)
        {
            n.tag = "Removed";
        }
        
        
    }

    void comeBack(GameObject flag)
    {


        subtitles.text = "No Towers Lets MoveToward Starting Point";
        transform.position = Vector3.MoveTowards(transform.position, flag.transform.position, 10 * Time.deltaTime); ;
    }


}
