using System;
using System.Collections.Generic;

/* Supports easier handling of languages in the clientMO
*/
public class Lang
{
    public string Code { get; set; }
    public string Name { get; set; }

    public Lang(string code, string name)
    {
        this.Code = code;
        this.Name = name;
    }

    public override string ToString()
    {
        return Name;
    }

}

public class Localization
{
    // Dictionary: language code → (dictionary: text key → localized string)
    private readonly Dictionary<string, Dictionary<string, string>> _languageDictionaries;

    private string _currentLanguage;

    public Localization()
    {
        _languageDictionaries = new Dictionary<string, Dictionary<string, string>>
        {
            { "en", new Dictionary<string, string> {
                { "menu_freeplay", "Free Play" },
                { "menu_campaign", "Start Campaign" },
                { "menu_load", "Load Game" },
                { "menu_options", "Options" },
                { "menu_quit", "Quit" },
                { "ingame_info_1", "Status.     " },
                { "ingame_info_2", "Generation: " },
                { "ingame_info_3", "Cell:       " },
                { "ingame_info_4", "MS / Tick:  " },
                { "ingame_info_5", "Zoom:       " },
                { "ingame_info_6", "Offset:     " },
                { "ingame_info_simulation_active", "Simulation running" },
                { "ingame_info_simulation_paused", "Simulation paused" },
                { "ingame_start", "Start Game" },
                { "ingame_pause", "Pause Game" },
                { "ingame_resume", "Resume Game" },
                { "ingame_generate", "Generate random field" },
                { "ingame_clear_field", "Clear field" },
                { "ingame_load_field", "Load Field" },
                { "ingame_save_field", "Save Field" },
                { "ingame_menu", "Main Menu" },
                { "ingame_rotate", "Rotate 90°" },
                { "ingame_mirror_x", "Mirror over X-axis" },
                { "ingame_mirror_y", "Mirror over Y-axis" },
                { "ingame_place_fig", "Paste figure" },
                { "ingame_place_cell", "Change cell" },
                { "ingame_pastedead", "Paste dead cells" },
                { "ingame_clear_fig", "Clear figure" },
                { "ingame_load_fig", "Load figure" },
                { "ingame_save_fig", "Save figure" },
                { "ingame_options", "Options" },
                { "options_fieldsize_label", "Field size\r\n\r\n(between 10 and 10.000)\r\nMust be larger than figure size. Use a comma (',') to separate width and height to create non-cubic fields." },
                { "options_figsize_label", "Figure size\r\n\r\n(between 2 and 100)\r\nMust be smalle than field size. Use a comma (',') to separate width and height to create non-cubic figures." },
                { "options_mspertick_label", "MS / Gametick\r\n\r\n(between 50 and 2000)\r\nSignificantly affects performance. It is not recommanded to use values under 100." },
                { "options_scrollspeed_label", "Scroll speed\r\n\r\n(between 1 and 50)\r\nDetermines the amount of pixels to scroll with each step. Scales with zoom level." },
                { "options_showgrid_label", "Display grid\r\n\r\nEnables / Disables the black grid on the field." },
                { "options_showgrid_checkbox", "Enable grid" },
                { "options_apply", "Apply changes" },
                { "options_lang", "Language" },
                { "options_back", "Back" },
                { "prompt_invalid_dimensions", "Invalid input! Specify one or two dimensions!" },
                { "prompt_invalid_format", "Invalid input! Specify value in whole numbers!" },
                { "prompt_override_field", "This action will overwrite the current game board." },
                { "prompt_fieldsize_changed", "Field size was changed." },
                { "prompt_figsize_changed", "Figure size was changed." },
                { "prompt_invalid_range", "Invalid input! Input outside the allowed range." },
                { "prompt_mspertick_changed", "MS / Tick have been adjusted." },
                { "prompt_scrollspeed_changed","Scroll speed has been adjusted." },
                { "lang_en", "English" },
                { "lang_fr", "French" },
                { "lang_de", "German" },
                { "lang_es", "Spanish" }

            }},
            { "fr", new Dictionary<string, string> {
                { "menu_freeplay", "Jeu Libre" },
                { "menu_campaign", "Démarrer la Campagne" },
                { "menu_load", "Charger la Partie" },
                { "menu_options", "Options" },
                { "menu_quit", "Quitter" },
                { "ingame_info_1", "Statut.      " },
                { "ingame_info_2", "Génération : " },
                { "ingame_info_3", "Cellule :    " },
                { "ingame_info_4", "MS / Tick :  " },
                { "ingame_info_5", "Zoom :       " },
                { "ingame_info_6", "Décalage :   " },
                { "ingame_info_simulation_active", "Simulation en cours" },
                { "ingame_info_simulation_paused", "Simulation en pause" },
                { "ingame_start", "Démarrer le Jeu" },
                { "ingame_pause", "Mettre en Pause" },
                { "ingame_resume", "Reprendre le Jeu" },
                { "ingame_generate", "Générer un champ aléatoire" },
                { "ingame_clear_field", "Vider le champ" },
                { "ingame_load_field", "Charger le Champ" },
                { "ingame_save_field", "Sauvegarder le Champ" },
                { "ingame_menu", "Menu Principal" },
                { "ingame_rotate", "Rotation 90°" },
                { "ingame_mirror_x", "Miroir sur l'axe X" },
                { "ingame_mirror_y", "Miroir sur l'axe Y" },
                { "ingame_place_fig", "Coller la figure" },
                { "ingame_place_cell", "Changer la cellule" },
                { "ingame_pastedead", "Coller les cellules mortes" },
                { "ingame_clear_fig", "Effacer la figure" },
                { "ingame_load_fig", "Charger la figure" },
                { "ingame_save_fig", "Sauvegarder la figure" },
                { "ingame_options", "Options" },
                { "options_fieldsize_label", "Taille du champ\r\n\r\n(entre 10 et 10 000)\r\nDoit être plus grande que la taille de la figure. Utilisez une virgule (',') pour séparer largeur et hauteur afin de créer des champs non cubiques." },
                { "options_figsize_label", "Taille de la figure\r\n\r\n(entre 2 et 100)\r\nDoit être plus petite que la taille du champ. Utilisez une virgule (',') pour séparer largeur et hauteur afin de créer des figures non cubiques." },
                { "options_mspertick_label", "MS / Tick de jeu\r\n\r\n(entre 50 et 2000)\r\nImpacte significativement la performance. Il n'est pas recommandé d'utiliser des valeurs inférieures à 100." },
                { "options_scrollspeed_label", "Vitesse de défilement\r\n\r\n(entre 1 et 50)\r\nDétermine le nombre de pixels défilés à chaque étape. Évolue avec le niveau de zoom." },
                { "options_showgrid_label", "Afficher la grille\r\n\r\nActive / Désactive la grille noire sur le champ." },
                { "options_showgrid_checkbox", "Activer la grille" },
                { "options_apply", "Appliquer les modifications" },
                { "options_lang", "Langue" },
                { "options_back", "Retour" },
                { "prompt_invalid_dimensions", "Entrée invalide ! Spécifiez une ou deux dimensions !" },
                { "prompt_invalid_format", "Entrée invalide ! Spécifiez la valeur en nombres entiers !" },
                { "prompt_override_field", "Cette action écrasera le plateau de jeu actuel." },
                { "prompt_fieldsize_changed", "La taille du champ a été modifiée." },
                { "prompt_figsize_changed", "La taille de la figure a été modifiée." },
                { "prompt_invalid_range", "Entrée invalide ! Valeur en dehors de la plage autorisée." },
                { "prompt_mspertick_changed", "MS / Tick ont été ajustés." },
                { "prompt_scrollspeed_changed", "La vitesse de défilement a été ajustée." },
                { "lang_en", "Anglais" },
                { "lang_fr", "Français" },
                { "lang_de", "Allemand" },
                { "lang_es", "Espagnol" }

            }},
            { "de", new Dictionary<string, string> {
                { "menu_freeplay", "Freies Spiel" },
                { "menu_campaign", "Kampagne starten" },
                { "menu_load", "Spiel laden" },
                { "menu_options", "Optionen" },
                { "menu_quit", "Beenden" },
                { "ingame_info_1", "Status.     " },
                { "ingame_info_2", "Generation: " },
                { "ingame_info_3", "Zelle:      " },
                { "ingame_info_4", "MS / Tick:  " },
                { "ingame_info_5", "Zoom:       " },
                { "ingame_info_6", "Offset:     " },
                { "ingame_info_simulation_active", "Simulation aktiv" },
                { "ingame_info_simulation_paused", "Simulation pausiert" },
                { "ingame_start", "Spiel starten" },
                { "ingame_pause", "Spiel pausieren" },
                { "ingame_resume", "Spiel fortführen" },
                { "ingame_generate", "Spielfeld zufällig generieren" },
                { "ingame_clear_field", "Spielfeld löschen" },
                { "ingame_load_field", "Spielfeld laden" },
                { "ingame_save_field", "Spielfeld speichern" },
                { "ingame_menu", "Hauptmenü" },
                { "ingame_rotate", "Um 90° drehen" },
                { "ingame_mirror_x", "Über X-Achse spiegeln" },
                { "ingame_mirror_y", "Über Y-Achse spiegeln" },
                { "ingame_place_fig", "Figur einfügen" },
                { "ingame_place_cell", "Zelle ändern" },
                { "ingame_pastedead", "inaktive Zellen einfügen" },
                { "ingame_clear_fig", "Figur löschen" },
                { "ingame_load_fig", "Figur laden" },
                { "ingame_save_fig", "Figur speichern" },
                { "ingame_options", "Optionen" },
                { "options_fieldsize_label", "Spielfeldgröße\r\n\r\n(zwischen 10 und 10.000)\r\nMuss größer als die Figurengröße sein. Verwende ein Komma (',') um Breite und Höhe bei nicht quadratischen Spielfeldern zu trennen." },
                { "options_figsize_label", "Figurengröße\r\n\r\n(zwischen 2 and 100)\r\nMuss kleiner als die Spielfeldgröße sein. Verwende ein Komma (',') um Breite und Höhe bei nicht quadratischen Figuren zu trennen." },
                { "options_mspertick_label", "MS / Gametick\r\n\r\n(zwischen 50 und 2000)\r\nHat signifikante Auswirkungen auf die Leistung. Es wird davon abgeraten Werte unter 100 zu verwenden." },
                { "options_scrollspeed_label", "Scrollgeschwindigkeit\r\n\r\n(zwischen 1 und 50)\r\nGibt an wie viele Pixel mit jedem Scrollschritt verschoben werden. Ist vom Zoomlevel abhängig." },
                { "options_showgrid_label", "Grid anzeigen\r\n\r\nAktiviert bzw. deaktiviert die Darstellung eines schwarzen Grids auf dem Spielfeld." },
                { "options_showgrid_checkbox", "Grid aktivieren" },
                { "options_apply", "Änderungen übernehmen" },
                { "options_lang", "Sprache" },
                { "options_back", "Zurück" },
                { "prompt_invalid_dimensions", "Ungültige Eingabe! Genau eine oder zwei Dimensionen angeben!" },
                { "prompt_invalid_format", "Ungültige Eingabe! Größe in ganzen numerischen Zahlen angeben!" },
                { "prompt_override_field", "Mit dieser Aktion wird das aktuelle Spielfeld überschrieben." },
                { "prompt_fieldsize_changed", "Spielfeldgröße wurde angepasst." },
                { "prompt_figsize_changed", "Figurengröße wurde angepasst." },
                { "prompt_invalid_range", "Ungültige Eingabe! Eingabe außerhalb des zulässigen Bereichs." },
                { "prompt_mspertick_changed", "MS / Tick wurden angepasst." },
                { "prompt_scrollspeed_changed","Scrollgeschwindigkeit wurde angepasst." },
                { "lang_en", "Englisch" },
                { "lang_fr", "Französisch" },
                { "lang_de", "Deutsch" },
                { "lang_es", "Spanisch" }
            }},
            { "es", new Dictionary<string, string> {
                { "menu_freeplay", "Juego Libre" },
                { "menu_campaign", "Iniciar Campaña" },
                { "menu_load", "Cargar Juego" },
                { "menu_options", "Opciones" },
                { "menu_quit", "Salir" },
                { "ingame_info_1", "Estado:         " },
                { "ingame_info_2", "Generación:     " },
                { "ingame_info_3", "Celda:          " },
                { "ingame_info_4", "MS / Tick:      " },
                { "ingame_info_5", "Zoom:           " },
                { "ingame_info_6", "Desplazamiento: " },
                { "ingame_info_simulation_active", "Simulación en ejecución" },
                { "ingame_info_simulation_paused", "Simulación en pausa" },
                { "ingame_start", "Iniciar Juego" },
                { "ingame_pause", "Pausar Juego" },
                { "ingame_resume", "Reanudar Juego" },
                { "ingame_generate", "Generar campo aleatorio" },
                { "ingame_clear_field", "Limpiar campo" },
                { "ingame_load_field", "Cargar Campo" },
                { "ingame_save_field", "Guardar Campo" },
                { "ingame_menu", "Menú Principal" },
                { "ingame_rotate", "Rotar 90°" },
                { "ingame_mirror_x", "Espejar sobre eje X" },
                { "ingame_mirror_y", "Espejar sobre eje Y" },
                { "ingame_place_fig", "Pegar figura" },
                { "ingame_place_cell", "Cambiar celda" },
                { "ingame_pastedead", "Pegar celdas muertas" },
                { "ingame_clear_fig", "Limpiar figura" },
                { "ingame_load_fig", "Cargar figura" },
                { "ingame_save_fig", "Guardar figura" },
                { "ingame_options", "Opciones" },
                { "options_fieldsize_label", "Tamaño del campo\r\n\r\n(entre 10 y 10,000)\r\nDebe ser mayor que el tamaño de la figura. Use una coma (',') para separar ancho y alto y crear campos no cúbicos." },
                { "options_figsize_label", "Tamaño de la figura\r\n\r\n(entre 2 y 100)\r\nDebe ser menor que el tamaño del campo. Use una coma (',') para separar ancho y alto y crear figuras no cúbicas." },
                { "options_mspertick_label", "MS / Tick del juego\r\n\r\n(entre 50 y 2000)\r\nAfecta significativamente el rendimiento. No se recomienda usar valores menores a 100." },
                { "options_scrollspeed_label", "Velocidad de desplazamiento\r\n\r\n(entre 1 y 50)\r\nDetermina la cantidad de píxeles desplazados en cada paso. Escala con el nivel de zoom." },
                { "options_showgrid_label", "Mostrar cuadrícula\r\n\r\nActiva / Desactiva la cuadrícula negra en el campo." },
                { "options_showgrid_checkbox", "Activar cuadrícula" },
                { "options_apply", "Aplicar cambios" },
                { "options_lang", "Idioma" },
                { "options_back", "Volver" },
                { "prompt_invalid_dimensions", "¡Entrada inválida! ¡Especifique una o dos dimensiones!" },
                { "prompt_invalid_format", "¡Entrada inválida! ¡Especifique el valor en números enteros!" },
                { "prompt_override_field", "Esta acción sobrescribirá el tablero de juego actual." },
                { "prompt_fieldsize_changed", "El tamaño del campo ha sido cambiado." },
                { "prompt_figsize_changed", "El tamaño de la figura ha sido cambiado." },
                { "prompt_invalid_range", "¡Entrada inválida! Valor fuera del rango permitido." },
                { "prompt_mspertick_changed", "Los MS / Tick han sido ajustados." },
                { "prompt_scrollspeed_changed", "La velocidad de desplazamiento ha sido ajustada." },
                { "lang_en", "Inglés" },
                { "lang_fr", "Francés" },
                { "lang_de", "Alemán" },
                { "lang_es", "Español" }
            }}
        };

        _currentLanguage = "en"; // Default language
    }

    public void SetLanguage(string languageCode)
    {
        if (_languageDictionaries.ContainsKey(languageCode))
        {
            _currentLanguage = languageCode;
        }
        else
        {
            throw new ArgumentException("Language not supported");
        }
    }

    public string GetText(string key)
    {
        if (_languageDictionaries.TryGetValue(_currentLanguage, out var translations))
        {
            if (translations.TryGetValue(key, out var localizedText))
            {
                return localizedText;
            }
        }
        // If no translation found, fallback to key or empty string
        return $"[{key}]";
    }
}