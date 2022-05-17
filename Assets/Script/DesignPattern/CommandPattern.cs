using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Command
{
    void Execute(Rigidbody rigid);
    void Undo(Rigidbody rigid);
}

public class Jump:Command
{
    public void Execute(Rigidbody rigid)
    {
        rigid.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
    }
    public void Undo(Rigidbody rigid)
    {
        rigid.AddForce(Vector3.up * -300.0f);
    }
}

public class Forward : Command
{
    public void Execute(Rigidbody rigid)
    {
        rigid.AddForce(Vector3.forward * 300.0f);
    }
    public void Undo(Rigidbody rigid)
    {
        rigid.AddForce(Vector3.forward * -300.0f);
    }
}

public class Backward : Command
{
    public void Execute(Rigidbody rigid)
    {
        rigid.AddForce(Vector3.forward * -300.0f);
    }
    public void Undo(Rigidbody rigid)
    {
        rigid.AddForce(Vector3.forward * 300.0f);
    }
}

public class Left : Command
{
    public void Execute(Rigidbody rigid)
    {
        rigid.AddForce(Vector3.left * 300.0f);
    }
    public void Undo(Rigidbody rigid)
    {
        rigid.AddForce(Vector3.left * -300.0f);
    }
}

public class Right : Command
{
    public void Execute(Rigidbody rigid)
    {
        rigid.AddForce(Vector3.right * 300.0f);
    }
    public void Undo(Rigidbody rigid)
    {
        rigid.AddForce(Vector3.right * -300.0f);
    }
}
public class CommandPattern : MonoBehaviour
{
    List<Command> commandlist = new List<Command>();
    Command space;
    Command K;
    Command W = new Forward();
    Command A = new Left();
    Command S = new Backward();
    Command D = new Right();
    // Start is called before the first frame update
    void Start()
    {
        space = new Jump();
        K = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            space?.Execute(GetComponent<Rigidbody>());
            commandlist.Add(space);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            K?.Execute(GetComponent<Rigidbody>());
            commandlist.Add(K);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            W?.Execute(GetComponent<Rigidbody>());
            commandlist.Add(W);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            A?.Execute(GetComponent<Rigidbody>());
            commandlist.Add(A);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            S?.Execute(GetComponent<Rigidbody>());
            commandlist.Add(S);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            D?.Execute(GetComponent<Rigidbody>());
            commandlist.Add(D);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Command temp = K;
            K = space;
            space = temp;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            commandlist[commandlist.Count - 1].Undo(GetComponent<Rigidbody>());
            commandlist.RemoveAt(commandlist.Count - 1);
        }
    }
}
