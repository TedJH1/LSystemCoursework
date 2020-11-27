using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// Transform information to allow returning of the LSystemGenerator when pushing to and popping from the stack
public class SavedTransform
{
    public Vector3 position;
    public Quaternion rotation;
}

public class LSystemGenerator : MonoBehaviour
{
    public Text axiom;
    public Text fRule;
    public Text xRule;
    public Slider angle;
    public Slider length;
    public Slider lengthLeaf;
    public Slider width;
    public Slider widthLeaf;
    public Text step;

    public GameObject branch;
    public GameObject leaf;

    private List<GameObject> tree;

    private Stack<SavedTransform> savedTransformStack;
    private string current;

    // Start is called before the first frame update
    void Start()
    {
        savedTransformStack = new Stack<SavedTransform>();
        tree = new List<GameObject>();
    }

    // Generate the L-tree
    public void Generate()
    {
        // Destroy the existing tree
        foreach (GameObject o in tree)
        {
            Destroy(o);
        }
        tree.Clear();

        // Reset the position of the LSystemGenerator
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        string axiomString = axiom.text;
        string fRuleString = fRule.text;
        string xRuleString = xRule.text;
        float angleFloat = angle.value;
        float lengthFloat = length.value;
        float lengthLeafFloat = lengthLeaf.value;
        float widthFloat = width.value;
        float widthLeafFloat = widthLeaf.value;
        int stepInt = Convert.ToInt32(step.text);

        current = axiomString;

        // Loops a number of times equal to the number of steps
        for (int i = 0; i < stepInt; i++)
        {
            // Algorithm replaces characters with their rule results or default value for each character
            StringBuilder sb = new StringBuilder();
            foreach (char c in current)
            {
                sb.Append(c == 'F' ? fRuleString : c == 'X' ? xRuleString : c.ToString());
            }

            current = sb.ToString();
        }

        foreach (char c in current)
        {
            // Each character does something specific
            switch (c)
            {
                // On 'F' save the starting position and move up by the value of length. Instantiate and draw a branch, with a preset width, from the start position to the current position
                case 'F':
                    Vector3 startPos = transform.position;
                    transform.Translate(Vector3.up * lengthFloat);
                    GameObject branchObj = Instantiate(branch);
                    tree.Add(branchObj);
                    LineRenderer lr = branchObj.GetComponent<LineRenderer>();
                    lr.startWidth = widthFloat;
                    lr.SetPosition(0, startPos);
                    lr.SetPosition(1, transform.position);
                    break;
                // On 'X' do nothing
                case 'X':
                    break;
                // On '+' rotate right by the preset angle
                case '+':
                    transform.Rotate(Vector3.forward * angleFloat);
                    break;
                // On '-' rotate left by the preset angle
                case '-':
                    transform.Rotate(Vector3.back * angleFloat);
                    break;
                // On '[' save the current position and rotation of the LSystemGenerator to the stack
                case '[':
                    savedTransformStack.Push(new SavedTransform()
                    {
                        position = transform.position,
                        rotation = transform.rotation
                    });
                    break;
                // On ']' instantiate and draw a leaf at the end of the branch. Then pop the most recent position and rotation from the stack and move the LSystemGenerator back to it
                case ']':
                    Vector3 startPosLeaf = transform.position;
                    transform.Translate(Vector3.up * lengthLeafFloat);
                    GameObject leafObj = Instantiate(leaf);
                    tree.Add(leafObj);
                    LineRenderer lr2 = leafObj.GetComponent<LineRenderer>();
                    lr2.startWidth = widthLeafFloat;
                    lr2.SetPosition(0, startPosLeaf);
                    lr2.SetPosition(1, transform.position);
                    SavedTransform st = savedTransformStack.Pop();
                    transform.position = st.position;
                    transform.rotation = st.rotation;
                    break;
                // Throw an exception if any other invalid symbol is used
                default:
                    throw new InvalidOperationException("Incorrect Character Entered");
            }
        }
    }
}
