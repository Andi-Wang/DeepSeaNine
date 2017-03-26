using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point{
    public int X { get; set; }
    public int Y { get; set; }
    

    public Point(int x, int y){
        this.X = x;
        this.Y = y;
    }

	//Get the next point in the given direction
	public Point getPointInDirection(Vector3 facing) {
		Point next = new Point(X + (int)facing.x, Y + (int)facing.y);
		return next;
	}

	public static int absoluteX(Point a, Point b){
		return Mathf.Abs (a.X - b.X);
	}

	public static int absoluteY(Point a, Point b){
		return Mathf.Abs (a.Y - b.Y);
	}
		
	public static int absoluteDistance(Point a, Point b){
		return absoluteX(a,b) + absoluteY(a,b);
	}
    
}
