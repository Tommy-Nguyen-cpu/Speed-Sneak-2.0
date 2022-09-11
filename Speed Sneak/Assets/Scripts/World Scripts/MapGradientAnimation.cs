using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGradientAnimation : MonoBehaviour
{
    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    private float currentTime = 0.0f;

    // Start and end index of the row and column iteration.
    int rowStart = 0;
    int rowEnd = 1;
    int rowIndexIncrement = 1;
    int rowEndIncrement = 1;

    int columnStart = 0;
    int columnEnd = 1;
    int columnIndexIncrement = 1;
    int columnEndIncrement = 1;

    int widthOfListOfBlocks;

    Func<int, int, bool> parameterComparator = (start, end) => start < end;
    Func<int, int, int, int> indexCalculator = (RowCurrent, TotalRows, ColumnCurrent) => RowCurrent * TotalRows + ColumnCurrent;


    float colorToChangeTo = 0.0f;
    float colorIncrement = .001f;

    GameObject[] listOfWallBlocks;
    // Start is called before the first frame update
    void Start()
    {
        listOfWallBlocks = GridScript.Row;

        // Assuming the grid of blocks is a square grid.
        widthOfListOfBlocks = (int)Math.Sqrt(listOfWallBlocks.Length);
        #region Setup Gradient
        gradient = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.magenta;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.cyan;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        // TODO: Successfully changes color, but the color is gradual. After each iteration, the previous color gets overridden (which we don't want). We want a gradient map.
        // Assuming the grid is square, we don't need to worry about checking columnToIterate since they are both the same.
        if (rowEnd > widthOfListOfBlocks)
        {
            rowStart = rowEnd-1;
            rowEnd = rowEnd-2;
            rowIndexIncrement = -1;
            rowEndIncrement = -1;

            columnStart = columnEnd - 1;
            columnEnd = columnEnd-2;
            columnEndIncrement = -1;
            columnIndexIncrement = -1;

            colorIncrement = -.001f;
            colorToChangeTo = 1.0f;

            parameterComparator = (startReverse, endReverse) => (startReverse >= endReverse);
            indexCalculator = (RowCurrent, TotalRows, ColumnCurrent) => TotalRows * (RowCurrent%TotalRows) + (ColumnCurrent%TotalRows);
        }
        else if (rowEnd < 0)
        {
            rowStart = 0;
            rowEnd = 1;
            rowIndexIncrement = 1;
            rowEndIncrement = 1;

            columnStart = 0;
            columnEnd = 1;
            columnIndexIncrement = 1;
            columnEndIncrement = 1;

            colorToChangeTo = 0.0f;
            colorIncrement = .001f;

            parameterComparator = (start, end) => start < end;
            indexCalculator = (RowCurrent, TotalRows, ColumnCurrent) => RowCurrent * TotalRows + ColumnCurrent;
        }


        GradientMapColorChange(parameterComparator, indexCalculator);

    }


    void GradientMapColorChange(Func<int, int, bool> startEndComparator, Func<int,int,int,int> indexCalculate)
    {
        // Change colors every .2 seconds.
        if (currentTime > .2f)
        {
            for (int currentRow = rowStart; startEndComparator(currentRow, rowEnd); currentRow += rowIndexIncrement)
            {
                for (int currentColumn = columnStart; startEndComparator(currentColumn, columnEnd); currentColumn += columnIndexIncrement)
                {
                    int index = indexCalculate(currentRow, widthOfListOfBlocks, currentColumn);

                    if (listOfWallBlocks[index] != null)
                    {
                        MeshRenderer render = listOfWallBlocks[index].GetComponent<MeshRenderer>();
                        colorToChangeTo += colorIncrement;
                        render.material.color = gradient.Evaluate(colorToChangeTo);
                    }
                }
            }

            rowEnd += rowEndIncrement;
            columnEnd += columnEndIncrement;

            

            currentTime = 0.0f;
        }
    }
}
