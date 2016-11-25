using UnityEngine;
using System.Collections;

public class DataManager : MonoBehaviour {
    string CONSOLELOG = "CONSOLE LOG";
    public string data1 = "data1";
    public string data2 = "data2";
    private IEnumerator getthedata;
    private IEnumerator postthedata;
    public string[] getdatavaluecontainor;
    void Start()
    {




        getthedata = Getdata();
        StartCoroutine(getthedata);

        postthedata = Postdata();
        StartCoroutine(postthedata);


    }

   
   

    private IEnumerator Getdata ()
    {
        WWW getData = new WWW("http://www.projectclickthrough.com/revgame/getdata.php?data1=data1&data2=data2");
        yield return getData;
        string GetTheData = getData.text;
        print(CONSOLELOG +" "+GetTheData);
        getdatavaluecontainor = GetTheData.Split(';');

        //we can get the datavale from each peram in our php script using the line below example get data 1
        print( Getdatavalue(getdatavaluecontainor[0],"Data1:"));

        //we can get the datavale from each peram in our php script using the line below example get data 2
        print(Getdatavalue(getdatavaluecontainor[0], "Data2:"));

        /* reffreace of the php script */

        /*
         
         <?php
//value example data value 1 and example value 2
if(isset($_GET['data1']) && isset($_GET['data2']))
{
	//set the vars of the get value passed along.
$data1= $_GET['data1'];
$data2= $_GET['data2'];
//echo formation of the format to return the corret results with out using an array or json
// use the | as the devider to seperate the new values as example below! 
 echo "Data1:".$data1."|Data2:".$data2;
 //so if this is true we can put this informtaion into the database
}
else
{
echo 0;
}

    ?>
         
         */

    }
    string Getdatavalue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index)+index.Length);
        if(value.Contains("|"))
        {
        value = value.Remove(value.IndexOf("|"));
        }
        
        return value;
    }

    private IEnumerator Postdata()
    {

        WWWForm postdata = new WWWForm();
        //postting the data to the server 
        //then its using php script to read data from the mysql database 
        //that stores the users data
        postdata.AddField(data1, "THE USERNAME");
        postdata.AddField(data2, "THE PASSWORD");

        WWW connection = new WWW("http://www.projectclickthrough.com/revgame/postdata.php", postdata);
        yield return connection;

        if (connection.isDone)
        {
            //this means the data was passed and its true 
            print(CONSOLELOG + " This is true " + connection.text);
        }
        else
        {
            // this means the data was not passed and we have a error
            print(CONSOLELOG + " This is False ");
        }

        /* reffreace of the php script */

        /*
         <?php

    if(isset($_POST['data1']) && isset($_POST['data2']))
{
$data1= $_POST['data1'];
$data2= $_POST['data2'];
 echo $data1." ".$data2;
 //so if this is true we can put this informtaion into the database
}
else
{
echo 0;
}

    ?>
         */


    }

    // Update is called once per frame
    void Update () {

      

    }
    
}
