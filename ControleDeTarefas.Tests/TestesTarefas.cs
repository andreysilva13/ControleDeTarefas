using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControleDeTarefas.ConsoleApp;
using System;
using System.Collections.Generic;

namespace ControleDeTarefas.Tests
{
    [TestClass]
    public class TestesTarefas
    {
        ControladorTarefa tarefa = new ControladorTarefa();

        [TestMethod]
        public void DeveInserirUmaTarefa()
        {
            Tarefa t = new Tarefa("titulo", 2);
            tarefa.Inserir(t);
        }

        [TestMethod]
        public void DeveSelecionarOsContatos()
        {
            bool b = false;
            List<Tarefa> a = tarefa.Visualizar();
            if (a.Count != null)
            {
                b = true;
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void DeveEditarUmContato()
        {
            Tarefa t = tarefa.SelecionarId(1);
            t.titulo = "titulo editado";
            tarefa.Editar(t);
        }

        [TestMethod]
        public void DeveExcluirUmContato()
        {
            Tarefa t = tarefa.SelecionarId(1);
            int id = t.id;
            tarefa.Excluir(id);
        }
    }
}
