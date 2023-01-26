using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu]
public class ClothesData : ScriptableObject
{
	[SerializeField]
	private int hair_hat; // the index of the prop
	[SerializeField]
	private int beard; // the index of the prop
	[SerializeField]
	private int top; // the index of the prop
	[SerializeField]
	private int bottom; // the index of the prop
	[SerializeField]
	private int shoes; // the index of the prop

	[SerializeField]
	private Color hair_hat_color; // the index of the prop
	[SerializeField]
	private Color beard_color; // the index of the prop
	[SerializeField]
	private Color top_color; // the index of the prop
	[SerializeField]
	private Color bottom_color; // the index of the prop
	[SerializeField]
	private Color shoes_color; // the index of the prop

	[SerializeField]
	private Animator top_control;
	[SerializeField]
	private Animator bottom_control;
	[SerializeField]
	private Animator shoes_control;

	[SerializeField]
	private RuntimeAnimatorController chosenTop;
	private RuntimeAnimatorController chosenBottom;
	private RuntimeAnimatorController chosenShoes;


    public Color HairColor { get { return hair_hat_color; } set { hair_hat_color = value; } }
	public Color BeardColor { get { return beard_color; } set { beard_color = value; } }
	public Color TopColor { get { return top_color; } set { top_color = value; } }
	public Color BottomColor { get { return bottom_color; } set { bottom_color = value; } }
	public Color ShoesColor { get { return shoes_color; } set { shoes_color = value; } }
    public int HairHat
	{
		get { return hair_hat; }
		set { hair_hat = value; }
	}
	public int Beard
	{
		get { return beard; }
		set { beard = value; }
	}
	public int Top
	{
		get { return top; }	
		set { top = value; }
	}
	public int Bottom
	{
		get { return bottom; }	
			set { bottom = value; }
	}
	public int Shoes
	{
		get { return shoes; }
        set {shoes = value;}
	}

	//animator controlers
	public Animator Top_anim { get { return top_control; } set { top_control = value; } }
	public Animator Bottom_anim { get { return bottom_control; } set { bottom_control = value; } }
	public Animator Shoes_anim { get { return shoes_control; } set { shoes_control = value; } }

	public RuntimeAnimatorController ChosenTop { get { return chosenTop; } set { chosenTop = value; } }
	public RuntimeAnimatorController ChosenBottom { get { return chosenBottom; } set { chosenBottom = value; } }
	public RuntimeAnimatorController ChosenShoes { get { return chosenShoes; } set { chosenShoes = value; } }

}
