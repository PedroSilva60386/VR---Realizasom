using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITest
{
    string GetTestName();
    Vector3 GetTestPosition();
    Vector3 GetTestVelocity();

    public void Complete();
    public bool HasFinished();

}

public class FirstTest : ITest
{
    private int totalTries = 10;
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
        return new Vector3(0, 0, -100f);
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

public class SecondTest : ITest
{
    private int totalTries = 5;
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
        return new Vector3(sign * 100, 0, -100f);
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
