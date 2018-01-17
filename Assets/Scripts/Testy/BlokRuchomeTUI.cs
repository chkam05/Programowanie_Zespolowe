﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ################################################################################

public class BlokRuchomeTUI : MonoBehaviour {

	// PRIVATE VARIABLES
	private	string			str_teoria				=	"    Brak teorii. ";
	private string			str_wzory				=	"";

	private	string			str_star				=	"    Uruchom bądź zakończ symulację. ";
	private string			str_achv				=	"    Pokaż okno postępów. ";
	private string			str_info				=	"    Pokaż informacje na temat tego zadania. ";
	private	string			str_exit				=	"    Wyjdź z trybu symulacji, podsumowując swój wynik. ";
	private	string			str_back				=	"    Poprzednia część testu: Bloki stałe. ";

	private int				size_wzory				=	10 * 32;

	// PUBLIC VARIABLES
	public	GameObject		module_game;

	public	GameObject		component_toolbar;
	public	GameObject		component_statusbar;
	public	GameObject		component_description;
	public	GameObject		component_inputBox;
	public	GameObject		component_messageQBox;
	public	GameObject		component_messageIBox;
	public	GameObject		component_settings;
	public	GameObject		component_welcome;

	public	GameObject		fieldInput_M1;
	public	GameObject		fieldInput_T;
	public	GameObject		fieldInput_r;
	public	GameObject		fieldInput_F1;
	public	GameObject		fieldInput_P;
	public	GameObject		fieldInput_Q;

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//    X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################

	void Start () {
		Time.timeScale	=	0.0f;

		component_settings.GetComponent<SettingsBox>().Setup();
		component_description.GetComponent<DescriptionBox>().Init( "Wzory", str_wzory, size_wzory );
		component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_teoria );

		component_toolbar.GetComponent<ToolBarBox>().setStartStopHover( onButtonEnter, onButtonExit );
		component_toolbar.GetComponent<ToolBarBox>().setAchivmnetsHover( onButtonEnter, onButtonExit );
		component_toolbar.GetComponent<ToolBarBox>().setInformationsHover( onButtonEnter, onButtonExit );
		component_toolbar.GetComponent<ToolBarBox>().setExitHover( onButtonEnter, onButtonExit );

		component_toolbar.GetComponent<ToolBarBox>().setStartStop( functionStart, functionStop, null, null );
		component_toolbar.GetComponent<ToolBarBox>().setInformations( "Wzory", str_wzory, size_wzory );

		fieldInput_M1.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
		fieldInput_M1.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick( onInputBox );
		fieldInput_M1.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );
		fieldInput_T.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseOver( onButtonEnter );
		fieldInput_T.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseClick( onInputBox );
		fieldInput_T.transform.GetChild(2).GetComponent<ButtonBehaviour>().setOnMouseExit( onButtonExit );

		component_statusbar.GetComponent<StatusBarBehaviour>().setPreviousFunction( onPreviousClick, str_back );
		component_welcome.GetComponent<WelcomeBox>().Init( "Bloki ruchome", "Test z bloków - część 2." );
		Init();
	}
	
	// ----------------------------------------------------------------------
	private void Init() {
		fieldInput_M1.GetComponent<InputField>().text	=	(20.0f).ToString();
		fieldInput_T.GetComponent<InputField>().text	=	(0.1f).ToString();
		fieldInput_r.GetComponent<InputField>().text	=	(2.0f).ToString();
	}

	// ----------------------------------------------------------------------
	void Update () {
		//
	}

	// ######################################################################
	//	X   X   XXXXX   X   X   XXXXX
	//	XX  X   X        X X      X  
	//	X X X   XXX       X       X  
	//	X  XX   X        X X      X  
	//	X   X   XXXXX   X   X     X  
	// ######################################################################

	private void onPreviousClick( object[] args ) {
		SceneManager.LoadScene( "Bloki Nieruchome Test" );
	}

	// ######################################################################
	//	XXX     X   X   XXXXX   XXXXX    XXX    X   X    XXXX
	//	X  X    X   X     X       X     X   X   XX  X   X    
	//	XXXX    X   X     X       X     X   X   X X X    XXX 
	//	X   X   X   X     X       X     X   X   X  XX       X
	//	XXXX     XXX      X       X      XXX    X   X   XXXX
	// ######################################################################

	private void onButtonEnter( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }
		var	current_button	=	args[0] as GameObject;

		if ( current_button.name == "Button StartStop" ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_star );
		} else if ( current_button.name == "Button Achivments" ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_achv );
		} else if ( current_button.name == "Button Informations" ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_info );
		} else if ( current_button.name == "Button Exit" ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_exit );

		} else if ( current_button == fieldInput_M1.transform.GetChild(3).gameObject ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( "" );
		} else if ( current_button == fieldInput_T.transform.GetChild(3).gameObject ) {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( "" );
		
		} else {
			component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_teoria );
		}
	}

	// ----------------------------------------------------------------------
	private void onButtonExit( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }

		component_statusbar.GetComponent<StatusBarBehaviour>().setInformations( str_teoria );
	}

	// ######################################################################
	//	XXXXX   X   X   XXXX    X   X   XXXXX      XXX      XXX    X   X
	//	  X     XX  X   X   X   X   X     X        X  X    X   X    X X 
	//	  X     X X X   XXXX    X   X     X        XXXX    X   X     X  
	//	  X     X  XX   X       X   X     X        X   X   X   X    X X 
	//	XXXXX   X   X   X        XXX      X        XXXX     XXX    X   X
	// ######################################################################

	private void onInputBox( object[] args ) {
		if ( Time.timeScale > 0.0f ) { return; }

		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }
		var	current_button	=	args[0] as GameObject;

		if ( current_button == fieldInput_M1.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Zmienna M:", "", "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldInput_M1 };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );
		
		} else if ( current_button == fieldInput_T.transform.GetChild(3).gameObject ) {
			string[]	str_texts	=	{ "Zmienna Alpha:", "", "Wprowadź", "Anuluj" };
			object[]	obj_result	=	new object[] { fieldInput_T };
			component_inputBox.GetComponent<InputBox>().Init( str_texts, ContentType.DecimalNumber, onInputBoxYes, null, obj_result );
		
		}
	}

	// ----------------------------------------------------------------------
	private void onInputBoxYes( string text, object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<InputField>() == null ) { return; }
		var		input_field		=	args[0] as GameObject;
		float	data;

		if ( !float.TryParse( input_field.GetComponent<InputField>().text, out data ) ) { return; }

		if ( input_field == fieldInput_T ) {
			if ( data < 0.0f || data > 1.0f ) { return; }
		}

		input_field.GetComponent<InputField>().text		=	text;
	}

	// ######################################################################
	//	 XXXX    XXX    X   X   XXXXX
	//	X       X   X   XX XX   X    
	//	X  XX   XXXXX   X X X   XXX  
	//	X   X   X   X   X   X   X    
	//	 XXXX   X   X   X   X   XXXXX
	// ######################################################################

	public void functionStart( object[] args ) {
		Time.timeScale			=	1.0f;
		
		setData();
		component_toolbar.GetComponent<ToolBarBox>().contentPosition( 0.0f );
	}

	// ----------------------------------------------------------------------
	public void functionStop( object[] args ) {
		Time.timeScale	=	0.0f;
		module_game.GetComponent<BlokRuchomeTest>().resetData();

		fieldInput_F1.GetComponent<InputField>().text	=	"";
		fieldInput_P.GetComponent<InputField>().text	=	"";
		fieldInput_Q.GetComponent<InputField>().text	=	"";
	}

	// ----------------------------------------------------------------------
	public void setData() {
		float	mass1		=	float.Parse( fieldInput_M1.GetComponent<InputField>().text );
		float	friction	=	float.Parse( fieldInput_T.GetComponent<InputField>().text );
		float	r			=	float.Parse( fieldInput_r.GetComponent<InputField>().text );
		module_game.GetComponent<BlokRuchomeTest>().setData( mass1, friction, r );
	}

	// ----------------------------------------------------------------------
	public void updateData( float f1, float p, float q ) {
		fieldInput_F1.GetComponent<InputField>().text	=	f1.ToString();
		fieldInput_P.GetComponent<InputField>().text	=	p.ToString();
		fieldInput_Q.GetComponent<InputField>().text	=	q.ToString();
	}

	// ######################################################################

}

// ################################################################################