using System;
using System.Collections.Generic;

namespace ToDoListConversion
{
    /// <summary>
    /// Todolist z pohledu systému Bakaláři. Umožňuje vytvořit a měnit definici todolistu.
    /// </summary>
    public class ToDoList 
    {
        /// <summary>
        /// Globální identifikátor todolistu.
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Název todolistu.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Seznam sekcí.
        /// </summary>
        public IList<ToDoSection> SectionSet { get; } = new List<ToDoSection>();
    }


    /// <summary>
    /// Sekce todolistu z pohledu systému Bakaláři
    /// </summary>
    public class ToDoSection 
    {
        /// <summary>
        /// Název sekce.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Globální identifikátor sekce
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Pořadí sekce v seznamu.
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// Seznam kontrolních bodů sekce.
        /// </summary>
        public IList<ToDoCheck> CheckSet { get; } = new List<ToDoCheck>();
        public string Number { get; set; }
    }


    /// <summary>
    /// Kontrolní bod todolistu z pohledu systému Bakaláři
    /// </summary>
    public class ToDoCheck 
    {
        /// <summary>
        /// Název kontrolního bodu.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Globální identifikátor kontrolního bodu
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Pořadí kontrolního bodu v sekci.
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// Popis toho, co je třeba v daném kontrolním bodě zkontrolovat.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Kontrolní bod je zastaralý a nebude již uživatelům zobrazován.
        /// </summary>
        public bool IsObsolete { get; set; }
        /// <summary>
        /// Klíčové slovo identifikující metodu akce, která se má provést při automatické kontrole
        /// </summary>
        public string Action { get; set; }
        public string Number { get; set; }
    }

}
