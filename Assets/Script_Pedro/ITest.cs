using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITest
{
    //Used to know the name of the test
    string GetTestName();
    Vector3 GetTestPosition();
    //Used to now the velocity of the puck in a certain test
    Vector3 GetTestVelocity();
    //Used to increase the current try of a test
    public void Complete();
    //Used to check if the test is over
    public bool HasFinished();

}

//FIRST TEST
public class FirstTest : ITest
{
    //Number of states we want in a certain test
    private int totalTries = 2;
    private int currentTry = 0;
    
    public string GetTestName()
    {
        return "Test 1 - " + currentTry + " - ";
    }

    public Vector3 GetTestPosition()
    {
        float x = Random.Range(-0.556f, 0.556f);
        return new Vector3(x, 0.7998f, 1.351f);
    }

    public Vector3 GetTestVelocity()
    {
        return new Vector3(0, 0, -75f);
    }

    public void Complete()
    {
        currentTry++;
    }

    public bool HasFinished()
    {
        return currentTry == totalTries;
    }
}
//SECOND TEST
public class SecondTest : ITest
{
    private int totalTries = 2;
    private int currentTry = 0;
    private Vector3 tempPosition;
    public string GetTestName()
    {
        return "Test 2 - " + currentTry + " - ";
    }

    public Vector3 GetTestPosition()
    {
        float x = Random.Range(-0.556f, 0.556f);
        tempPosition = new Vector3(x, 0.7998f, 1.351f);
        return tempPosition;
    }

    public Vector3 GetTestVelocity()
    {
        float sign = tempPosition.x > 0.0 ? -1.0f : 1.0f;
        return new Vector3(sign * 75, 0, -75);
    }

    public void Complete()
    {
        currentTry++;
    }

    public bool HasFinished()
    {
        return currentTry == totalTries;
    }
}
//THIRD TEST
public class ThirdTest : ITest
{
    private int totalTries = 2;
    private int currentTry = 0;
    private Vector3 tempPosition;
    public string GetTestName()
    {
        return "Test 3 - " + currentTry + " - ";
    }

    public Vector3 GetTestPosition()
    {
        float x = Random.Range(-0.556f, 0.556f);
        tempPosition = new Vector3(x, 0.7998f, 1.351f);
        return tempPosition;
    }

    public Vector3 GetTestVelocity()
    {
        float sign = tempPosition.x > 0.0 ? 1.0f : -1.0f;
        return new Vector3(sign * 75, 0, -75);
    }

    public void Complete()
    {
        currentTry++;
    }

    public bool HasFinished()
    {
        return currentTry == totalTries;
    }
}
//FOURTH TEST
public class FourthTest : ITest
{
    private int totalTries = 2;
    private int currentTry = 0;
    private Vector3 tempPosition;
    public string GetTestName()
    {
        return "Test 4 - " + currentTry + " - ";
    }

    public Vector3 GetTestPosition()
    {
        float x = Random.Range(-0.556f, 0.556f);
        tempPosition = new Vector3(x, 0.7998f, 1.351f);
        return tempPosition;
    }

    public Vector3 GetTestVelocity()
    {
        var direction = Random.Range(-1.0f, 1.0f);
        return new Vector3(direction * 75, 0, -75);
    }

    public void Complete()
    {
        currentTry++;
    }

    public bool HasFinished()
    {
        return currentTry == totalTries;
    }
}