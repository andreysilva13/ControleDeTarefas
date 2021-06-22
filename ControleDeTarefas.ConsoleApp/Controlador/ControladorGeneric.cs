using System.Collections.Generic;
using ControleDeTarefas.ConsoleApp.Dominio;

namespace ControleDeTarefas.ConsoleApp.Controlador
{
    public abstract class ControladorGeneric<T> where T : EntidadeBase
    {
        public ControladorGeneric()
        {
        }

        public void Inserir(T registro)
        {
            ObterInsert(registro);
        }

        public void Editar(T registro)
        {
            ObterEdicao(registro);
        }

        public void Excluir(int id)
        {
            object tipo = id.GetType();
            ObterExclusao(tipo, id);
        }

        public List<T> Visualizar()
        {
            List<T> lista = ObterRegistros();
            return lista;
        }

        public T SelecionarId(int id)
        {
            T item = ObterIdSelecionado(id);
            return item;
        }

        public abstract void ObterInsert(T registro);
        public abstract void ObterEdicao(T registro);
        public abstract void ObterExclusao(object tipo, int id);
        public abstract List<T> ObterRegistros();
        public abstract T ObterIdSelecionado(int id);
    }

    
}
