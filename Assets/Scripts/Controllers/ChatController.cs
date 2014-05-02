using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public abstract class ChatMessage
{
    public abstract void OnGUI();
}

public class TextMessage : ChatMessage
{
    string Sender;
    string Message;

    public TextMessage(string sender, string message)
    {
        Sender = sender;
        Message = message;
    }

    public override void OnGUI()
    {
        GUILayout.Label(Sender + ": " + Message);
    }
}



public class Roll
{
    public List<int> Results = new List<int>();
    public int Count;
    public int Die;
    static Random Random = new Random();

    public Roll(string rollText)
    {
        Count = int.Parse(rollText.Split('d')[0]);
        Die = int.Parse(rollText.Split('d')[1].Split('(')[0]);
        if (!rollText.Contains("("))
        {
            for (int i = 0; i < Count; i++)
            {
                Results.Add(Random.Range(1, Die));
            }
        }
        else
        {
            string[] vals = rollText.Split('(')[1].Split(')')[0].Split(',');
            foreach (string val in vals)
            {
                Results.Add(int.Parse(val));
            }
        }
    }
}

public class RollMessage : ChatMessage
{
    string Sender;
    string Serialized;
    string Original;
    List<Roll> Rolls = new List<Roll>();
    int Result;

    public RollMessage(string sender, string text)
    {
        Sender = sender;
        Original = text;
        string[] parts = text.Split(' ');

        int positive = 1;
        int total = 0;
        bool isDie = false;
        StringBuilder builder = new StringBuilder();
        StringBuilder serializedBuilder = new StringBuilder();

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            if (c == ' ' || c == '+' || c == '-')
            {
                if (builder.Length > 0)
                {
                    if (isDie)
                    {
                        Roll roll = new Roll(builder.ToString());
                        Rolls.Add(roll);
                        serializedBuilder.Append(builder.ToString() + "(");
                        foreach (int value in roll.Results)
                        {
                            serializedBuilder.Append(value + ",");
                            total += positive * value;
                        }
                        serializedBuilder.Remove(serializedBuilder.Length - 1, 1); // remove the , at end
                        serializedBuilder.Append(")");
                        serializedBuilder.Append(' ');
                        builder = new StringBuilder();
                        isDie = false;
                    }
                    else
                    {
                        total += positive * int.Parse(builder.ToString());
                        serializedBuilder.Append(builder.ToString() + " ");
                        builder = new StringBuilder();
                        isDie = false;
                    }
                }

                if (c == '+')
                {
                    positive = 1;
                    serializedBuilder.Append("+ ");
                }

                if (c == '-')
                {
                    positive = -1;
                    serializedBuilder.Append("- ");
                }
            }
            else
            {
                builder.Append(c);
                if (c == 'd')
                {
                    isDie = true;
                }
            }
        }
        if (builder.Length > 0)
        {
            if (isDie)
            {
                Roll roll = new Roll(builder.ToString());
                Rolls.Add(roll);
                serializedBuilder.Append(builder.ToString() + "(");
                foreach (int value in roll.Results)
                {
                    serializedBuilder.Append(value + ",");
                    total += positive * value;
                }
                serializedBuilder.Remove(serializedBuilder.Length - 1, 1); // remove the , at end
                serializedBuilder.Append(")");
                serializedBuilder.Append(' ');
                builder = new StringBuilder();
                isDie = false;
            }
            else
            {
                total += positive * int.Parse(builder.ToString());
                serializedBuilder.Append(builder.ToString() + " ");
                builder = new StringBuilder();
                isDie = false;
            }
        }
        Result = total;
        Serialized = serializedBuilder.ToString();
    }

    public string OutputString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(Sender + " rolled " + Result + " (" + Original + ")");
        return builder.ToString();
    }

    public string SerializedString()
    {
        return Serialized;
    }
    
    public override void OnGUI()
    {
        GUILayout.BeginHorizontal(GUILayout.ExpandWidth(false));
        string[] parts = Serialized.Split(' ');
        GUILayout.Label(Sender + " Rolled " + Result + " ( ");
        foreach (string part in parts)
        {
            if (!part.Contains("d"))
            {
                GUILayout.Label(part);
            }
            else
            {
                GUILayout.Label(new GUIContent(part.Split('(')[0], part.Split('(')[1].Split(')')[0]));
            }
        }
        GUILayout.Label(")");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}

public class ChatController : MonoBehaviour {

    public SessionManager SessionManager;
    public List<ChatMessage> Messages = new List<ChatMessage>();

	// Use this for initialization
	void Start () {
	
	}

    public void AddText(string text)
    {
        if (SessionManager.Active)
        {
            networkView.RPC("SendTextMessage", RPCMode.AllBuffered, SessionManager.UserName, text);
        }
        else
        {
            SendTextMessage(SessionManager.UserName, text);
        }
    }

    public bool AddRoll(string rollText)
    {
        try
        {
            RollMessage rollMessage = new RollMessage(SessionManager.UserName, rollText);
            if (SessionManager.Active)
            {
                networkView.RPC("SendRollMessage", RPCMode.AllBuffered, SessionManager.UserName, rollMessage.SerializedString());
            }
            else
            {
                SendRollMessage(SessionManager.UserName, rollMessage.SerializedString());
            }
            return true;
        }
        catch { }
        return false;
    }

    [RPC]
    void SendTextMessage(string user, string message)
    {
        Messages.Add(new TextMessage(user, message));
    }

    [RPC]
    void SendRollMessage(string user, string serialized)
    {
        Messages.Add(new RollMessage(user, serialized));
    }

	// Update is called once per frame
	void Update () {
	
	}
}
