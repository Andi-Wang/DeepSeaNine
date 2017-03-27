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

	public static bool operator ==(Point lhs, Point rhs){
		if (lhs.X == rhs.X && lhs.Y == rhs.Y) {
			return true;
		}
		return false;
	}

	public static bool operator !=(Point lhs, Point rhs){
		if (lhs.X == rhs.X && lhs.Y == rhs.Y) {
			return false;
		}
		return true;
	}

/* Nothing but trouble!!
	public override bool Equals(object obj){
		if (obj == null || this.GetType () != obj.GetType ()) {
			return false;
		}
		return this == ((Point)obj);
	}

CAUSED STACK OVERFLOW:
	public override int GetHashCode(){
		return this.GetHashCode ();
	}
*/
	//Get the next point in the given direction
	public Point getPointInDirection(Vector3 facing) {
		if (facing == Vector3.down || facing == Vector3.up) {
			facing *= -1;
		}
		Point next = new Point(X + (int)facing.x, Y + (int)facing.y);
		return next;
	}

	// returns direction to point if they are adjacent
	public Vector3 getDirectionTo(Point p) {
		if (absoluteX (this, p) == 1) {
			return Vector3.right;
		} else if (absoluteX (this, p) == -1) {
			return Vector3.left;
		} else if (absoluteY (this, p) == -1) {
			return Vector3.up;
		} else if (absoluteY (this, p) == 1) {
			return Vector3.down;
		}
		return Vector3.zero;
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

	public override string ToString ()
	{
		return string.Format ("[Point: X={0}, Y={1}]", X, Y);
	}
    
}
