#include <stdafx.h>
static void OnStart()
{
	AUDIO::PLAY_END_CREDITS_MUSIC(1);
	AUDIO::SET_MOBILE_RADIO_ENABLED_DURING_GAMEPLAY(1);
	AUDIO::SET_MOBILE_PHONE_RADIO_STATE(1);
	AUDIO::SET_RADIO_TO_STATION_NAME("RADIO_16_SILVERLAKE");
	AUDIO::SET_CUSTOM_RADIO_TRACK_LIST("RADIO_16_SILVERLAKE", "END_CREDITS_SAVE_MICHAEL_TREVOR", 1);
	REQUEST_SCRIPT("finale_credits");
	while (!HAS_SCRIPT_LOADED("finale_credits"))
	{
		WAIT(0);
	}
	START_NEW_SCRIPT("finale_credits", 1424); // just took a guess on the int, no idea what it does.
}

static void OnStop()
{
	AUDIO::PLAY_END_CREDITS_MUSIC(0);
	AUDIO::SET_MOBILE_RADIO_ENABLED_DURING_GAMEPLAY(0);
	AUDIO::SET_MOBILE_PHONE_RADIO_STATE(0);
	MISC::SET_CREDITS_ACTIVE(0);
	TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME("finale_credits");
	//Reset everything finale_credits sets to restore playability.
	ENTITY::SET_ENTITY_INVINCIBLE(PLAYER::PLAYER_PED_ID(), false);
	ENTITY::SET_ENTITY_VISIBLE(PLAYER::PLAYER_PED_ID(), true, 0);
	ENTITY::FREEZE_ENTITY_POSITION(PLAYER::PLAYER_PED_ID(), false);
	PED::SET_ENABLE_SCUBA(PLAYER::PLAYER_PED_ID(), false);
	PLAYER::SET_MAX_WANTED_LEVEL(5);
	PLAYER::SET_PLAYER_CONTROL(PLAYER::PLAYER_ID(), true, 0);
	HUD::DISPLAY_HUD(true);
	HUD::DISPLAY_RADAR(true);
}

static RegisterEffect registerEffect(EFFECT_MISC_CREDITS, OnStart, OnStop, nullptr);