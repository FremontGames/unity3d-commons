﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public enum InputDirection
{
    Left, Right, Top, Bottom
}
public enum GameState
{
    Playing,
    Won,
    Loss
}

public enum HorizontalMovement
{
    Left, Right
}

public enum VerticalMovement
{
    Top, Bottom
}

public interface IGameEngine
{
    // string[][] turn(string[][] inputs);
}

// https://dgkanatsios.com/2016/01/23/building-the-2048-game-in-unity-via-c-and-visual-studio/
public class GameManager : IGameEngine
{
    int width;
    int height;
    string[][] matrix;

    GameState state;
    int score;

    string move;
    HorizontalMovement horizontalMovement;

    public string[][] init(string[][] input)
    {
        width = Int32.Parse(input[0][0]);
        height = Int32.Parse(input[0][1]);
        //res[0] = new string[] { input[0][0], input[0][1] };
        rule_init_matrix_with_0();
        rule_init_matrix_with_2_random_items();
        score = 0;
        state = GameState.Playing;
        return matrix;
    }

    public string[][] turn(string[][] input)
    {
        string[][] res;

        width = Int32.Parse(input[0][0]);
        height = Int32.Parse(input[0][1]);
        matrix = new string[height][];
        for (int y = 0; y < height; y++)
        {
            matrix[y] = input[1 + y];
        }
        move = input[1 + height][0];
        horizontalMovement = HorizontalMovement.Right;
        rule_turn_move_items();
        // TODO UpdateScore(0);
        return matrix;
    }

    private void rule_turn_move_items()
    {

        // TODO is can move
        //    rule_turn_merge_same_items();
        rule_turn_move_items_to_horizontal_direction_if_free();

    }

    // RULES ********************************************************

    private void rule_init_matrix_with_0()
    {
        matrix = new string[height][];
        for (int y = 0; y < height; y++)
        {
            matrix[y] = new string[width];
            for (int x = 0; x < width; x++)
            {
                matrix[y][x] = "0";
            }
        }
    }

    private void rule_init_matrix_with_2_random_items()
    {
        createRandomItem(matrix);
        createRandomItem(matrix);
    }

    private void rule_turn_merge_same_items()
    {
        throw new NotImplementedException();
    }

    private void rule_turn_move_items_to_horizontal_direction_if_free()
    {
        var columnNumbers = Enumerable.Range(0, width);
        int dir = (horizontalMovement == HorizontalMovement.Left) ? -1 : 1;
        columnNumbers = (horizontalMovement == HorizontalMovement.Left) ? columnNumbers : columnNumbers.Reverse();
        for (int y = 0; y < height; y++)
        {
            foreach (int x in columnNumbers)
            {
                if ("0".Equals(matrix[y][x]))
                    continue;
                int nextColumn = nextFreePosition(matrix[y], x, dir);
                matrix[y][nextColumn] = matrix[y][x];
                matrix[y][x] = "0";
            }
        }

    }
    private int nextFreePosition(string[] row, int x, int direction)
    {
        int free = x;
        bool hasFree = true;
        while (hasFree)
        {
            int next = free+direction;
            bool isNotOutOfBound = (next < row.Length && next > -1);
            hasFree = isNotOutOfBound && "0".Equals(row[next]);
            if (hasFree)
                free = next;
            else
                return free;
        }
        return free;
    }

    // UTILS ********************************************************

    public int count(string[][] matrix, string match)
    {
        int res = 0;
        for (int y = 0; y < matrix.Length; y++)
        {
            for (int x = 0; x < matrix[0].Length; x++)
            {
                if (match.Equals(matrix[y][x]))
                    res++;
            }
        }
        return res;
    }

    private void createRandomItem(string[][] matrix)
    {
        int freeOnes = count(matrix, "0");
        Random rnd = new Random();
        int nextOne = rnd.Next(0, freeOnes);
        int i = 0;
        for (int y = 0; y < matrix.Length; y++)
        {
            for (int x = 0; x < matrix[0].Length; x++)
            {
                if ("0".Equals(matrix[y][x]))
                    i++;
                if (nextOne == i)
                    matrix[y][x] = "2";
            }
        }
    }
}
