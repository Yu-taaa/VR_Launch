using UnityEngine;
using System.Collections;

public class ViveController : MonoBehaviour {

//	var device = SteamVR_Controller.Input ((int)trackedObject.index);
//	Ray ray = new Ray (transform.position, transform.forward);
//	if(Physics.Raycast(ray, out hit)) {
//		print (hit.collider.name);
//	}

//
//	if (Input.GetMouseButton (0)) {
//		//左クリック
//		print("aaa");
//		Instantiate (cube,new Vector3 (0, 5, 0), Quaternion.identity);
//	}
//
//	if(Input.GetMouseButton(1)){
//		//右クリック
//		print("bbb");
//	}

	public static void ChangeRightHand(){
		//タッチパッドの右を選択でHand 左を選択でPenLight
//		if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad)){
//			if (device.GetAxis ().x > 0) {
//				//タッチパッドの右側選択
//			} else if (device.GetAxis ().x < 0) {
//				//タッチパッドの左を選択
//			}
//	    }

	}

	public static void ChangeLeftHand(){
		//タッチパッドの右を選択でHand 左を選択でPenLight
		//		if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad)){
		//			if (device.GetAxis ().x > 0) {
		//				//タッチパッドの右側選択
		//			} else if (device.GetAxis ().x < 0) {
		//				//タッチパッドの左を選択
		//			}
		//	    }

	}


	public static void ChangeLight(){
		//トリガー引いてライト選択→popアップ 色選択
//		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
//			Debug.Log("トリガーを深く引いた");
//		}

	}

	public static void ChangeBrightness(){
	}

	public static void ChangeCamera(){
		//トリガーひいてカメラもつ
		//トリガー離してカメラ離す
//		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
//			Debug.Log("トリガーを深く引いた");
//		}

//		if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
//			Debug.Log("トリガーを離した");
//		}
		
	}

	public static void ChangeZoom(){
	}

	public static void ChangeEffect(){
		//トリガーを引くとeffect発動
//		if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
//			Debug.Log("トリガーを深く引いた");
//		}
		
	}

	public static void MakeExplosion(){
	}
}
