﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ################################################################################
//	 XXXX   XXXXX    XXX    XXXXX   X   X    XXXX      XXX      XXX    XXXX 
//	X         X     X   X     X     X   X   X          X  X    X   X   X   X
//	 XXX      X     XXXXX     X     X   X    XXX       XXXX    XXXXX   XXXX 
//	    X     X     X   X     X     X   X       X      X   X   X   X   X   X
//	XXXX      X     X   X     X      XXX    XXXX       XXXX    X   X   X   X
//
//	XXX     XXXXX   X   X    XXX    X   X   XXXXX    XXX    X   X   XXXX 
//	X  X    X       X   X   X   X   X   X     X     X   X   X   X   X   X
//	XXXX    XXX     XXXXX   XXXXX   X   X     X     X   X   X   X   XXXX 
//	X   X   X       X   X   X   X    X X      X     X   X   X   X   X   X
//	XXXX    XXXXX   X   X   X   X     X     XXXXX    XXX     XXX    X   X
// ################################################################################

public class StatusBarBehaviour : MonoBehaviour {

	// PRIVATE VARIABLES:
	private ActionTimerExpired		functionTimerExpired	=	null;
	private ActionButtonClick		functionButtonNext		=	null;
	private ActionButtonClick		functionButtonPrevious	=	null;
	private	object[]				objectsTimerExpired		=	null;

	private	int						timer_max				=	0;
	private	int						timer					=	0;
	private int						timer_detector			=	0;
	private	bool					timer_reverse			=	false;
	private	string					string_informations		=	"    Informacje testowe StatusBar. ";
	private	string					string_settings			=	"    Otwórz okno ustawień. ";
	private	string					string_exit				=	"    Natychmiastowy powrót do menu. ";
	private string					string_previous			=	"    Powrót do poprzedniej części. ";
	private string					string_next				=	"    Przejście do następnej części. ";

	// PUBLIC VARIABLES:
	public	GameObject				button_settings;
	public	GameObject				button_exit;
	public	GameObject				button_previous;
	public	GameObject				button_next;
	public	GameObject				text_informations;
	public	GameObject				text_timer;

	public	GameObject				component_settings;
	public	GameObject				component_messageQBox;

	public	delegate void			ActionTimerExpired( object[] args );
	public	delegate void			ActionButtonClick( object[] args );

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	  X     XX  X     X       X  
	//	  X     X X X     X       X  
	//	  X     X  XX     X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################

	void Start () {
		button_settings.GetComponent<ButtonBehaviour>().setOnMouseOver( ButtonMouseOverBehavior );
		button_settings.GetComponent<Button>().onClick.AddListener( onSettingsClick );
		button_settings.GetComponent<ButtonBehaviour>().setOnMouseExit( ButtonMouseExitBehaviour );

		button_exit.GetComponent<ButtonBehaviour>().setOnMouseOver( ButtonMouseOverBehavior );
		button_exit.GetComponent<Button>().onClick.AddListener( onExitClick );
		button_exit.GetComponent<ButtonBehaviour>().setOnMouseExit( ButtonMouseExitBehaviour );

		text_informations.GetComponent<TextRoll>().setText( string_informations );
	}

	// ----------------------------------------------------------------------
	void Update() {
		workTimer();
	}

	// ######################################################################
	//	XXXXX   XXXXX   X   X   XXXXX
	//	  X     X        X X      X  
	//	  X     XXX       X       X  
	//	  X     X        X X      X  
	//	  X     XXXXX   X   X     X  
	// ######################################################################

	public void setInformations( string text ) {
		this.string_informations	=	text;
		changeInformations( text );
	}

	// ----------------------------------------------------------------------
	private void changeInformations( string text ) {
		text_informations.GetComponent<TextRoll>().setText( text );
	}

	// ######################################################################
	//	XXXXX   XXXXX   X   X   XXXXX   XXXX 
	//	  X       X     XX XX   X       X   X
	//	  X       X     X X X   XXX     XXXX 
	//	  X       X     X   X   X       X   X
	//	  X     XXXXX   X   X   XXXXX   X   X
	// ######################################################################

	public void initTimer( int maxTimeSec, bool reverseTime, ActionTimerExpired functionTimerExpired, object[] args ) {
		this.timer_max				=	maxTimeSec;
		this.timer_reverse			=	reverseTime;
		this.functionTimerExpired	=	functionTimerExpired;
		this.objectsTimerExpired	=	args;
		this.timer					=	0;
	}

	// ----------------------------------------------------------------------
	private void workTimer() {
		int		seconds		=	System.DateTime.Now.Second;
		
		if ( timer_detector != seconds ) {
			timer_detector		=	seconds;

			if ( timer_reverse ) {
				timer			=	timer - 1;
				if ( timer <= 0 ) { endTimer(); }
			} else {
				timer			=	timer + 1;
				if ( timer >= timer_max ) { endTimer(); }
			}

			int		int_sec		=	timer % 60;
			int		int_min		=	timer / 60 % 60;
			int		int_hou		=	timer / 60 / 60;

			string	str_time	=	((int_hou<10)?"0"+int_hou.ToString():int_hou.ToString()) + ":"
								+	((int_min<10)?"0"+int_min.ToString():int_min.ToString()) + ":"
								+	((int_sec<10)?"0"+int_sec.ToString():int_sec.ToString());
			text_timer.GetComponent<Text>().text	=	str_time;
		}
	}

	// ----------------------------------------------------------------------
	private void endTimer() {
		if ( timer_max <= 0 ) { return; }
		if ( functionTimerExpired != null ) { functionTimerExpired( objectsTimerExpired ); }
	}

	// ######################################################################
	//	XXX     X   X   XXXXX   XXXXX    XXX    X   X    XXXX
	//	X  X    X   X     X       X     X   X   XX  X   X    
	//	XXXX    X   X     X       X     X   X   X X X    XXX 
	//	X   X   X   X     X       X     X   X   X  XX       X
	//	XXXX     XXX      X       X      XXX    X   X   XXXX
	// ######################################################################

	public void setNextFunction( ActionButtonClick function ) {
		functionButtonNext	=	function;
	}

	// ----------------------------------------------------------------------
	public void setPreviousFunction( ActionButtonClick function ) {
		functionButtonPrevious	=	function;
	}

	// ----------------------------------------------------------------------
	public void onNextButtonClick() {
		if ( functionButtonNext != null ) { functionButtonNext( new object[] { } ); }
	}

	// ----------------------------------------------------------------------
	public void onPreviousButtonClick() {
		if ( functionButtonPrevious != null ) { functionButtonPrevious( new object[] { } ); }
	}

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX
	//	X        X X      X       X  
	//	XXX       X       X       X  
	//	X        X X      X       X  
	//	XXXXX   X   X   XXXXX     X  
	// ######################################################################

	private void onExitClick() {
		string[]	str_data	=	{ "Wyjdź do menu głównego", "Czy na pewno chcesz opuścić tryb interaktywny, bez sprawdzania?", "Tak, wyjdź", "Nie, zostań" };
		component_messageQBox.GetComponent<MessageQBox>().Init( str_data, onExitYesClick, null, null );
	}

	// ----------------------------------------------------------------------
	private void onExitYesClick( object[] args ) {
		SceneManager.LoadScene( "Main Menu" );
	}

	// ######################################################################
	//	 XXXX   XXXXX   XXXXX   XXXXX   XXXXX   X   X    XXXX    XXXX
	//	X       X         X       X       X     XX  X   X       X    
	//	 XXX    XXX       X       X       X     X X X   X  XX    XXX 
	//	    X   X         X       X       X     X  XX   X   X       X
	//	XXXX    XXXXX     X       X     XXXXX   X   X    XXXX   XXXX 
	// ######################################################################

	private void onSettingsClick() {
		component_settings.GetComponent<SettingsBox>().Init( true );
	}

	// ######################################################################
	//	XXXXX   X   X   XXXXX   XXXXX   XXXX     XXX     XXX    XXXXX   XXXXX    XXX    X   X
	//	  X     XX  X     X     X       X   X   X   X   X   X     X       X     X   X   XX  X
	//	  X     X X X     X     XXX     XXXX    XXXXX   X         X       X     X   X   X X X
	//	  X     X  XX     X     X       X   X   X   X   X   X     X       X     X   X   X  XX
	//	XXXXX   X   X     X     XXXXX   X   X   X   X    XXX      X     XXXXX    XXX    X   X
	// ######################################################################

	private void ButtonMouseOverBehavior( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }
		var	current_button	=	args[0] as GameObject;

		if ( current_button == button_settings ) {
			changeInformations( string_settings );
		} else if ( current_button == button_exit ) {
			changeInformations( string_exit );
		} else if ( current_button == button_previous ) {
			changeInformations( string_previous );
		} else if ( current_button == button_next ) {
			changeInformations( string_next );
		}
	}

	// ----------------------------------------------------------------------
	private void ButtonMouseExitBehaviour( object[] args ) {
		if ( args.Length <= 0 ) { return; }
		if ( args[0].GetType() != typeof(GameObject) ) { return; }
		if ( (args[0] as GameObject).GetComponent<Button>() == null ) { return; }

		changeInformations( this.string_informations );
	}

	// ######################################################################

}

// ################################################################################