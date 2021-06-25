using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControleDeTarefas.ConsoleApp.Controlador;
using ControleDeTarefas.ConsoleApp;
using ControleDeTarefas.ConsoleApp.Dominio;
using System.Collections.Generic;
using System;

namespace ControleDeTarefas.Tests
{
    [TestClass]

    public class TestesCompromisso
    {
        ControladorCompromisso compromisso = new ControladorCompromisso();

        [TestMethod]
        public void DeveAtribuirUmCompromisso()
        {
            DateTime dataInicio = new DateTime(2020, 08, 29, 22, 23, 21);
            DateTime dataTermino = new DateTime(2020, 08, 29, 22, 23, 21);
            Compromissos c = new Compromissos("nome", "email", "telefone", dataInicio, dataTermino, 2);
            compromisso.Inserir(c);
        }

        [TestMethod]
        public void DeveEditarUmCompromisso()
        {
            Compromissos c = compromisso.SelecionarId(3);
            c.assunto = "assunto editado";
            compromisso.Editar(c);
        }

        [TestMethod]
        public void DeveExcluirUmCompromisso()
        {
            Compromissos c = compromisso.SelecionarId(3);
            int id = c.id;
            compromisso.Excluir(id);
        }
    }
}
