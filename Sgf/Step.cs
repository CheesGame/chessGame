﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     如果重新生成代码，将丢失对此文件所做的更改。
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public class Step
{
    //记录棋谱
    private Piece[] step;
    //记录总共下的步数
    private int numberOfStep;

    public Step()
    {
        step = new Piece[10000];
        numberOfStep = 0;
    }
    public Piece[] getStep()
    {
        return this.step;
    }
    public void setStep()
    {
    }
    public int getNumberOfStep()
    {
        return this.numberOfStep;
    }
    public void setNumberOfStep(int numberOfStep)
    {
        this.numberOfStep = numberOfStep;
    }
    //下一步棋
    public void addStep(Piece newStep)
    {
        step[numberOfStep] = new Piece();
        step[numberOfStep] = newStep;
        numberOfStep++;
    }
    //退一步棋
    public void rollBack()
    {
        numberOfStep--;
    }

    //读入sgf文件并解读
    public void fromFile(string path)
    {
        //读文件
        FileStream f = new FileStream(path, FileMode.Open);
        StreamReader r = new StreamReader(f);
        string fileString = r.ReadToEnd();
        r.Close();
        f.Close();
        String[] fileSplit = new String[10000];
        fileSplit=fileString.Split(';');
        for (int i = 0; i <fileSplit.Length; i++) {
            if (fileSplit[i].Length < 2) continue;
            if (!(fileSplit[i][0] == 'B'&& fileSplit[i][1]=='[') || (fileSplit[i][0] == 'W'&&fileSplit[i][1]=='[')) continue;
            Piece stone = new Piece();
            if (fileSplit[i][0] == 'B') stone.setColor(1);
            else stone.setColor(-1);
            stone.setXCoordinate(fileSplit[i][2] - 'a' + 1);
            stone.setYCoordinate(fileSplit[i][3] - 'a' + 1);
            addStep(stone);
        }
    }
    public void saveToFile(string path)
    {
        String content = "(;GM[1]FF[4]SZ[19]DT["+System.DateTime.Now+"]KM[0.0]AP[GNU Go:3.6];";
        content = content + toString()+")";
        StreamWriter sw = File.CreateText(path);
        sw.WriteLine(content);
        sw.Close();
    }
    public String toString() {
        String content="";
        for (int i = 0; i < numberOfStep; i++) {
            if (step[i].getColor() == -1) content = content + "W";
            else content = content + "B";
            content = content + "[" + ('a' + step[i].getXCoordinate() - 1) + "," + ('a' + step[i].getYCoordinate() - 1) + "]" + ";";
        }
        return content;
    }
}

