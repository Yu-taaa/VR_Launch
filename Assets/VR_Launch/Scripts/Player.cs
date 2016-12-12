using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public ViveController rightcontroller;
	public ViveController leftcontroller;
	public PlayerStatus.ActorType playerstatus; 



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
	{
		//playerによってcontrollerの属性を変える
		switch (playerstatus) {
		case PlayerStatus.ActorType.nomal:
			//役職なし
			rightcontroller.ChangeRightHand;
			leftcontroller.ChangeLeftHand;
		case PlayerStatus.ActorType.light:
			//照明
			rightcontroller.ChangeLight;
			leftcontroller.ChangeBrightness;
		case PlayerStatus.ActorType.camera:
			//カメラ
			rightcontroller.ChangeCamera;
			leftcontroller.ChangeZoom;

		case PlayerStatus.ActorType.effect:
			//効果
			rightcontroller.ChangeEffect;
			leftcontroller.MakeExplosion;
		}

	}
}
