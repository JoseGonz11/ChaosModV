﻿using System.Collections.Generic;

namespace ConfigApp
{
    class esES
    {
        public static readonly Dictionary<string, string> PopupMSGs = new Dictionary<string, string>()
        {
            { "no_write_permission_msg", "No hay permisos para escribir en el directorio actual. Intente ejecutar el programa como administrador o permitir el acceso de escritura al directorio actual." },
            { "no_write_permission_title", "Sin permiso de escritura" },
            { "reset_click_msg", "¿Estás seguro de que quieres restablecer tu configuración?" },
            { "reset_twitch_msg", "¿Desea restablecer su configuración de Twitch también?" },
            { "reset_complete_msg", "¡La configuración se ha restablecido a los valores predeterminados!" },
            { "save_complete_msg", "Configuración guardada!\nAsegúrate de presionar CTRL + L en el juego dos veces si el mod ya se está ejecutando para volver a cargar la configuración." },
        };
        public static string[] voteModes = new string[]
        {
            "Mensajes En Chat",
            "Interfaz Ingame",
            "Interfaz OBS"
        };
        public static readonly Dictionary<string, string> App = new Dictionary<string, string>()
        {
            { "title", "ChaosModV - Configuración"},
            { "user_save", "Guardar"},
            { "user_reset", "Reiniciar"},
            { "lang_label", "Idioma" },
            { "effects_tab", "Efectos" },
            { "meta_tab", "Meta" },
            { "misc_tab", "Misceláneo" },
            { "twitch_tab", "Twitch"},
            { "more_tab", "Más"},
            { "no_update_available_label", "¡Estás en la versión más reciente del mod!" },
            { "update_available_label", "¡Una nueva version esta disponible! Visite el GitHub para ver las descargas y los cambios." },
            { "update_error", "Error al buscar nuevas actualizaciones!"},
            { "meta_cooldown_label", "Tiempo entre efectos meta (segundos) "},
            { "meta_duration_label", "Duración de efectos meta por tiempo (segundos)?" },
            { "meta_short_label", "Duración de efectos meta por tiempo cortos (segundos)?" },
            { "effect_timer", "Tiempo para nuevo efecto (segundos)"},
            { "effect_duration", "Duración de efectos por tiempo (segundos)"},
            { "effect_duration_short", "Duración de efectos cortos (segundos)"},
            { "effects_seed", "Semilla (dejar vacío para ser totalmente aleatorio)"},
            { "timebar_color", "Color Barra de tiempo"},
            { "effect_textcolor", "Color Texto de efecto"},
            { "effect_timer_color", "Color Duración de efecto"},
            { "timebar_draw", "No dibujar barra de tiempo"},
            { "effect_text_draw", "No dibujar texto de efecto"},
            { "clear_effects", "Habilitar atajo para limpiar todos los efectos (CTRL + -)"},
            { "mod_toggle", "Habilitar interruptor de mod (CTRL + L)"},
            { "effect_menu", "Activar menú de efectos (Elige efectos manualmente abrir con CTRL +,)"},
            { "timebar_pause", "Habilitar pausa (CTRL + .)" },
            { "twitch_info", "Esta función permite que el chat de Twitch vote un efecto de una colección de efectos aleatorios cada vez que se agota el tiempo al usar la función de chat de Twitch.\nHay opciones específicas que puede / debe configurar a continuación.\nConsulte el archivo twitch__readme.txt incluido con el mod (dentro de la carpeta README) para obtener más información." },
            { "twitch_user_agreed", "Habilitar la votación de Twitch" },
            { "twitch_user_channel_name_label", "Nombre del Canal" },
            { "twitch_user_user_name_label", "Nombre de usuario" },
            { "twitch_user_channel_oauth_label", "Token de OAuth" },
            { "twitch_user_effects_secs_before_chat_voting_label", "Habilitar la votación por chat X segundos antes del efecto\n(0 = Sin restricciones. Debe ser mayor a 1)" },
            { "twitch_user_overlay_mode_label", "Modo de superposición de votaciones" },
            { "twitch_user_random_voteable_enable_label", "Habilitar la opción votable \"Efecto aleatorio\"" },
            { "proportional_vote", "---------- Modo de votación proporcional ----------" },
            { "twitch_user_chance_system_enable_label", "Habilitar el modo de votación proporcional" },
            { "twitch_user_chance_system_retain_chance_enable_label", "Los efectos sin votos mantienen su probabilidad inicial" },
            { "mod_page", "Visita la página del mod para obtener más información." },
            { "donation", "¿Disfrutando del mod? ¡Doname un café! :)" },
            { "contribute", "¿Quieres contribuir al mod? ¡Es de código abierto!" },
            { "discord", "¡Únete a nuestro Discord!" },
            { "translatedby", "Traducido por JoseGonz11" }
        };
        public static readonly Dictionary<int, string> EffectsCategoryLabels = new Dictionary<int, string>()
        {
            {0, "Jugador" },
            {1, "Vehículo" },
            {2, "Peatones" },
            {3, "Hora" },
            {4, "Clima" },
            {5, "Misceláneo" },
            {6, "Meta" }
        };
        public static readonly Dictionary<string, string> EffectsMap = new Dictionary<string, string>()
        {
            {"player_suicide","Suicidio" },
            {"player_plus2stars","+2 Estrellas Se Busca" },
            {"player_5stars","5 Estrellas Se Busca" },
            {"player_neverwanted","La Poli Te Ignora" },
            {"peds_remweps","Quitar Armas A Todos" },
            {"player_heal","Salud, Armadura y 250k $" },
            {"player_ignite", "Prender Fuego Al Jugador" },
            {"spawn_grieferjesus", "Spawn Jesucristo Campero" },
            {"peds_spawnimrage", "Spawn Impotent Rage" },
            {"spawn_grieferjesus2", "Spawn Jesucristo Campero Extremo" },
            {"peds_ignite", "Prender Fuego A Los Personajes Cercanos"},
            {"vehs_explode", "Reventar Vehiculos Cercanos"},
            {"player_upupaway", "Lanzar Jugador Arriba"},
            {"vehs_upupaway", "Lanzar Todos los Vehiculos Arriba"},
            {"playerveh_lock", "Encerrar Jugador En Vehiculo Actual"},
            {"nothing", "Nada"},
            {"playerveh_killengine", "Romper Motor De Todos Los Vehiculos"},
            {"time_morning", "Establecer Hora En El Amanecer"},
            {"time_day", "Establecer Hora En El Mediodia"},
            {"time_evening", "Establecer Hora En El Atardecer"},
            {"time_night", "Establecer Hora En La Medianoche"},
            {"weather_extrasunny", "Clima Extra Soleado"},
            {"weather_stormy", "Clima tormentoso"},
            {"weather_foggy", "Clima Con Niebla"},
            {"weather_neutral", "Clima Neutro"},
            {"weather_snowy", "Clima De Nieve"},
            {"tp_lsairport", "Teletransporte Al Aeropuerto De LS"},
            {"tp_mazebanktower", "Teletransporte A Encima De La Torre Maze"},
            {"tp_fortzancudo", "Teletransporte A Base Militar"},
            {"tp_skyfall", "Teletransporte Al Cielo"},
            {"tp_mountchilliad", "Teletransporte Al Monte Chilliad"},
            {"tp_random", "Teletransporte Aleatorio"},
            {"tp_mission", "Teletransporte A Mision Aleatoria"},
            {"tp_fake", "Teletransporte Falso"},
            {"player_nophone", "Sin Telefono"},
            {"player_tpclosestveh", "Meter Jugador En Vehiculo Mas Cercano"},
            {"playerveh_exit", "Todos Salen De Sus Vehiculos"},
            {"time_x02", "Velocidad De Juego x0.2"},
            {"time_x05", "Velocidad De Juego x0.5"},
            {"time_lag", "Lag"},
            {"peds_riot", "Modo Disturbios"},
            {"vehs_red", "Trafico Rojo"},
            {"vehs_blue", "Trafico Azul"},
            {"vehs_green", "Trafico Verde"},
            {"vehs_chrome", "Trafico Cromado"},
            {"vehs_pink", "Trafico Rosa"},
            {"vehs_rainbow", "Trafico Arcoiris"},
            {"player_firstperson", "Primera Persona"},
            {"vehs_slippery", "Vehiculos Resbalosos"},
            {"vehs_nogravity", "Los Vehiculos No Tienen Gravedad"},
            {"player_invincible", "Invencibilidad"},
            {"vehs_x2engine", "Velocidad De Motor x2"},
            {"vehs_x10engine", "Velocidad De Motor x10"},
            {"vehs_x05engine", "Velocidad De Motor x0.5"},
            {"spawn_rhino", "Spawn Tanque"},
            {"spawn_adder", "Spawn Super Deportivo"},
            {"spawn_dump", "Spawn Camion De Carga"},
            {"spawn_monster", "Spawn Monster Truck"},
            {"spawn_bmx", "Spawn Bici"},
            {"spawn_tug", "Spawn Barco De Pesca"},
            {"spawn_cargo", "Spawn Avion De Carga"},
            {"spawn_bus", "Spawn Bus"},
            {"spawn_blimp", "Spawn Zepelin"},
            {"spawn_buzzard", "Spawn Helicoptero De Combate"},
            {"spawn_faggio", "Spawn Motillo"},
            {"spawn_ruiner3", "Spawn Ruiner Arruinado"},
            {"spawn_baletrailer", "Spawn Trailer De Heno"},
            {"spawn_romero", "Donde es el funeral?"},
            {"spawn_random", "Spawn Vehiculo Aleatorio"},
            {"notraffic", "Sin Trafico"},
            {"playerveh_explode", "Reventar Vehiculo Actual"},
            {"peds_ghost", "Todo El Mundo Es Un Fantasma"},
            {"vehs_ghost", "Vehiculos Invisibles"},
            {"no_radar", "Sin Radar"},
            {"no_hud", "Sin Interfaz"},
            {"player_superrun", "Super Carrera & Super Salto"},
            {"player_ragdoll", "El Jugador Se Cae"},
            {"peds_ragdoll", "Todos Se Caen"},
            {"peds_sensitivetouch", "Sensible A Los Roces"},
            {"poorboi", "Bancarrota"},
            {"player_famous", "Eres Famoso"},
            {"player_drunk", "Borracho"},
            {"player_ohko", "1 De Vida"},
            {"screen_bloom", "Bloom"},
            {"screen_lsd", "LSD"},
            {"screen_lowrenderdist", "Donde Han Ido Todos?"},
            {"screen_fog", "Niebla Extrema"},
            {"screen_lsnoire", "LS Noire"},
            {"screen_bright", "Vision Frita"},
            {"screen_mexico", "Es Asi Como Se Ve Mexico?"},
            {"screen_fullbright", "A Tope De Brillo"},
            {"screen_bubblevision", "Vision De Burbuja"},
            {"peds_blind", "Personajes Cegatos"},
            {"vehs_honkboost", "Turbo Claxon"},
            {"peds_mindmg", "No Hay Dolor"},
            {"peds_frozen", "Personajes Bobos"},
            {"lowgravity", "Baja Gravedad"},
            {"verylowgravity", "Muy Baja Gravedad"},
            {"insanegravity", "Gravedad Insana"},
            {"invertgravity", "Gravedad Invertida"},
            {"playerveh_repair", "Reparar Todos Los Vehiculos"},
            {"playerveh_poptires", "Reventar Todos Los Neumaticos"},
            {"vehs_poptiresconstant", "Esto Si Que Son Neumaticos Reventando"},
            {"player_nospecial", "Sin Habilidad especial"},
            {"peds_dance", "Temazo, Loco"},
            {"player_forcedcinematiccam", "Camara Cinematica En Vehiculos"},
            {"peds_flee", "Todos Huyen"},
            {"playerveh_breakdoors", "Romper Todas Las Puertas"},
            {"zombies", "Zombies Explosivos"},
            {"meteorrain", "Ducha De Meteoritos"},
            {"world_blackout", "Apagon"},
            {"time_quickday", "Timelapse"},
            {"player_noforwardbackward", "Desactivar Movimiento Adelante / Atras"},
            {"player_noleftright", "Desactivar Movimiento Izquierda / Derecha"},
            {"player_break", "Piloto Automatico"},
            {"peds_giverpg", "Dar A Todos Un RPG"},
            {"peds_stungun", "Dar A Todos Una Taser"},
            {"peds_minigun", "Dar A Todos Una Minigun"},
            {"peds_upnatomizer", "Dar A Todos Un Arma Alienigena"},
            {"peds_randomwep", "Dar A Todos Un Arma Aleatoria"},
            {"peds_railgun", "Dar A Todos Un Arma De Rayos"},
            {"peds_battleaxe", "Dar A Todos Un Hacha De Batalla"},
            {"player_nosprint", "Sin Sprint & Sin Salto"},
            {"peds_invincible", "Todo El Mundo Es Invencible"},
            {"vehs_invincible", "Vehiculos Invulnerables"},
            {"player_ragdollondmg", "Sensible A Los Tiros"},
            {"vehs_jumpy", "Vehiculos Saltarines"},
            {"vehs_lockdoors", "Bloquear Todos Los Vehiculos"},
            {"chaosmode", "Dia Del Juicio Final"},
            {"player_noragdoll", "Equilibrio De Acero"},
            {"vehs_honkconstant", "Todos Los Vehiculos Pitan"},
            {"player_tptowaypoint", "Teletransporte Al Punto De Ruta"},
            {"peds_sayhi", "Vecindario Amigable"},
            {"peds_insult", "Vecindario Desagradable"},
            {"player_explosivecombat", "Combate Explosivo"},
            {"player_allweps", "Dar Todas Las Armas"},
            {"peds_aimbot", "Ciudadanos Con Aimbot"},
            {"spawn_chop", "Spawn Perro Aliado"},
            {"spawn_chimp", "Spawn Simio Aliado"},
            {"spawn_compbrad", "Spawn Brad Aliado"},
            {"spawn_comprnd", "Spawn Aliado Aleatorio"},
            {"player_nightvision", "Vision Nocturna"},
            {"player_heatvision", "Vision Termica"},
            {"player_moneydrops", "Lluvia De Dinero"},
            {"playerveh_tprandompeds", "Meter Gente Aleatoria En El Vehiculo"},
            {"peds_revive", "Revivir Personajes Muertos"},
            {"world_snow", "Nieve"},
            {"world_whalerain", "Lluvia De Ballenas"},
            {"playerveh_maxupgrades", "Mejorar Todos Los Vehiculos Al Maximo"},
            {"playerveh_randupgrades", "Mejorar Todos Los Vehiculos Aleatoriamente"},
            {"player_arenawarstheme", "Reproducir Cancion De Arena Wars"},
            {"peds_driveby", "Te Disparan Desde El Coche"},
            {"player_randclothes", "Ropa Aleatoria"},
            {"peds_rainbowweps", "Armas Arcoiris"},
            {"traffic_gtao", "Iman De Trafico"},
            {"spawn_bluesultan", "Spawn Sultan Azul Enemigo"},
            {"player_setintorandveh", "Meter Jugador En Vehiculo Aleatorio"},
            {"traffic_fullaccel", "Gas, Gas, Gas"},
            {"misc_spawnufo", "Spawn OVNI"},
            {"misc_spawnferriswheel", "Spawn Noria"},
            {"peds_explosive", "Peatones Explosivos"},
            {"invertvelocity", "Invertir Velocidad Actual"},
            {"player_tpeverything", "Traer Todo Al Jugador"},
            {"weather_randomizer", "Clima Disco"},
            {"world_lowpoly", "Graficos De PS1"},
            {"peds_obliterate", "Todos Los Peatones Explotan"},
            {"vehs_alarmloop", "Suenan Las Alarmas"},
            {"veh_randomseat", "Cambiar De Asiento Al Jugador"},
            {"veh_30mphlimit", "Limitador De Velocidad 30MPH"},
            {"veh_jesustakethewheel", "Jesus Coge El Volante"},
            {"veh_poptire", "Las Ruedas Estallan Aleatoriamente"},
            {"peds_angryalien", "Spawn Alien Enfadado"},
            {"peds_angryjimmy", "Spawn Jimmy El Celoso"},
            {"vehs_ohko", "Vehiculos Explotan Al Impactar"},
            {"vehs_spamdoors", "Puertas Automaticas"},
            {"veh_speed_goal", "Need For Speed"},
            {"vehs_flyingcars", "Coches Voladores"},
            {"misc_lester", "Pwned"},
            {"misc_credits", "Pasar Los Creditos"},
            {"misc_earthquake", "Terremoto"},
            {"player_tpfront", "Teletransportar Jugador Adelante"},
            {"peds_spawnfancats", "Spawn Gatos Amistosos"},
            {"peds_cops", "Todos Los Peatones Son Policias"},
            {"vehs_rotall", "Voltear Todos Los Vehiculos"},
            {"peds_launchnearby", "Lanzar Peatones Arriba"},
            {"peds_attackplayer", "Todos Atacan Al Jugador"},
            {"player_clone", "Clonar Jugador"},
            {"peds_slidy", "Peatones Deslizantes"},
            {"peds_spawndancingapes", "Spawn Simios Bailarines"},
            {"misc_onebullet", "Cargadores De Una Bala"},
            {"peds_phones", "De Quien Es El Movil?"},
            {"misc_midas", "Toque Dorado"},
            {"peds_spawnrandomhostile", "Spawn Enemigo Aleatorio"},
            {"playerveh_nobrakes", "Sin Frenos"},
            {"peds_portal_gun", "Armas De Portales"},
            {"misc_fireworks", "Fuegos Artificiales!"},
            {"peds_spawnballasquad", "Spawn Grupo Balla"},
            {"playerveh_despawn", "Borrar Vehiculo Actual"},
            {"peds_scooterbrothers", "Hermanos De Motillo"},
            {"peds_intorandomvehs", "Meter A Todos En Vehiculos Aleatorios"},
            {"player_heavyrecoil", "Gran Retroceso De Las Armas"},
            {"peds_catguns", "Armas Gatunas"},
            {"player_forcefield", "Campo De Fuerza"},
            {"misc_oilleaks", "Rastros De Aceite"},
            {"peds_gunsmoke", "Armas Humeantes"},
            {"player_keeprunning", "Ayuda, No Puedo Parar"},
            {"veh_weapons", "Vehiculos Con Lanzamisiles"},
            {"misc_airstrike", "Ataque Aereo En Camino"},
            {"peds_mercenaries", "Mercenarios"},
            {"peds_loosetrigger", "Gatillo Suelto"},
            {"peds_minions", "Minions"},
            {"misc_flamethrower", "Lanzallamas"},
            {"misc_dvdscreensaver", "Fondo De Pantalla DVD"},
            {"player_fakedeath", "Muerte Falsa"},
            {"time_superhot", "Superhot"},
            {"vehs_beyblade", "Beyblades"},
            {"peds_killerclowns", "Payasos Asesinos"},
            {"peds_jamesbond", "Spawn Agente Mortifero"},
            {"player_poof", "Punteria Letal"},
            {"player_simeonsays", "Simeon Dice"},
            {"veh_lockcamera", "Bloquear Camara En Vehiculos"},
            {"misc_replacevehicle", "Intercambiar Vehiculo Actual"},
            {"player_tired", "Estoy Tan Cansado..."},
            {"player_kickflip", "Kickflip"},
            {"misc_superstunt", "Super Acrobacia"},
            {"player_walkonwater", "Andar Sobre El Agua"},
            {"screen_needglasses", "Se Me Han Olvidado Las Gafas"},
            {"player_flip_camera", "Camara Invertida"},
            {"player_quake_fov", "Campo De Vision A Tope"},
            {"player_rapid_fire", "Fuego Rapido"},
            {"player_on_demand_cartoon", "Netflix And Chill"},
            {"peds_drive_backwards", "Ciudadanos Conducen Marcha Atras"},
            {"veh_randtraffic", "Trafico Aleatorio"},
            {"misc_rampjam", "Salto Con Rampa"},
            {"misc_vehicle_rain", "Lluvia De Vehiculos"},
            {"misc_fakecrash", "Crasheo Falso"},
            {"player_gravity", "Campo Gravitatorio"},
            {"veh_bouncy", "Vehiculos Rebotantes"},
            {"peds_stop_stare", "Parate Y Mira"},
            {"peds_flip", "Peatones Rotatorios"},
            {"player_pacifist", "Pacifista"},
            {"peds_busbois", "Los Chicos Del Bus"},
            {"player_dead_eye", "Ojo Tuerto"},
            {"player_hacking", "Hackeo Realista"},
            {"peds_nailguns", "Pistolas de clavos"},
            {"veh_brakeboost", "Turbo Freno"},
            {"player_bees", "ABEJAS!"},
            {"player_vr", "Modo VR"},
            {"misc_portrait", "Modo Retrato"},
            {"misc_highpitch", "Te Me Bajas 2 Tonitos, Nota"},
            {"misc_nosky", "Sin Cielo"},
            {"player_gta_2", "GTA 2"},
            {"peds_kifflom", "Kifflom!"},
            {"meta_timerspeed_0_5x", "Velocidad x0.5 De Cuenta Atras"},
            {"meta_timerspeed_2x", "Velocidad x2 De Cuenta Atras"},
            {"meta_timerspeed_5x", "Velocidad x5 De Cuenta Atras"},
            {"meta_effect_duration_2x", "x2 Duracion De Efectos"},
            {"meta_effect_duration_0_5x", "x0.5 Duracion De Efectos"},
            {"meta_hide_chaos_ui", "Que Esta Pasando??"},
            {"meta_spawn_multiple_effects", "Combo De Efectos"},
            {"vehs_crumble", "Vehiculos Hechos Chatarra"},
            {"misc_fps_limit", "Experiencia De Consola"},
            {"meta_nochaos", "Sin Caos"},
            {"peds_roasting", "Humillacion"}
        };
    }
}