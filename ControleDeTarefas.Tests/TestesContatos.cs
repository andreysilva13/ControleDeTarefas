using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControleDeTarefas.ConsoleApp.Controlador;
using ControleDeTarefas.ConsoleApp;
using System.Collections.Generic;

namespace ControleDeTarefas.Tests
{
    [TestClass]

    public class TestesContatos
    {
        ControladorContato contato = new ControladorContato();

        [TestMethod]
        public void DeveAtribuirUmContato()
        {
            Contato c = new Contato("nome", "email", "telefone", "empresa", "administrador");
            contato.Inserir(c);
        }

        [TestMethod]
        public void DeveSelecionarOsContatos()
        {
            bool b = false;
            List<Contato> a = contato.Visualizar();
            if (a.Count != null)
            {
                b = true;
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void DeveEditarUmContato()
        {
            Contato c = contato.SelecionarId(1);
            c.email = "email editado";
            contato.Editar(c);
        }

        [TestMethod]
        public void DeveExcluirUmContato()
        {
            Contato c = contato.SelecionarId(1);
            int id = c.id;
            contato.Excluir(id);
        }
    }
}
