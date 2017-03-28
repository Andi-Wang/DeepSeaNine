using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInfo {

	public Point point;
	public NodeInfo parent;
	public int g, h;

	public int f(){
		return g + h;
	}

	public NodeInfo(Point p, NodeInfo par, int ag, int ah){
		point = p;
		parent = par;
		g = ag;
		h = ah;

	}
	
}
