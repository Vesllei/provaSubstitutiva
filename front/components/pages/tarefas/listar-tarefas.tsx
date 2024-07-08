import React, { useEffect, useState } from 'react';
import { Tarefa } from '../../../models/Tarefa';
import { Link } from 'react-router-dom';

function ListarTarefas() {
  const [tarefas, setTarefas] = useState<Tarefa[]>([]);

  useEffect(() => {
    carregarTarefas();
  }, []);

  async function carregarTarefas() {
    try {
      const response = await fetch('http://localhost:5028/api/tarefa/listar');
      if (!response.ok) {
        throw new Error('Erro ao buscar tarefas');
      }
      const data = await response.json();
      setTarefas(data);
    } catch (error) {
      console.error('Erro ao carregar tarefas:', error);
    }
  }

  async function Delete(id: string) {
    try {
      const response = await fetch(`http://localhost:5028/api/tarefa/deletar/${id}`, {
        method: 'DELETE',
      });
      if (!response.ok) {
        throw new Error('Erro ao deletar tarefa');
      }
      carregarTarefas();
    } catch (error) {
      console.error('Erro ao deletar tarefa:', error);
    }
  }

  return (
    <div>
      
      <h1>Listar Tarefas</h1>
      <table>
        <thead>
          <tr>
            <th>#</th>
            <th>Título</th>
            <th>Descrição</th>
            <th>Prazo</th>
            <th>Prioridade</th>
            <th>Projeto</th>
            <th>Atribuir</th>
            <th>Alterar</th>
            <th>Remover</th>
          </tr>
        </thead>
        <tbody>
          {tarefas.map((tarefa) => (
            <tr key={tarefa.id}>
              <td>{tarefa.id}</td>
              <td>{tarefa.titulo}</td>
              <td>{tarefa.descricao}</td>
              <td>{tarefa.prazo}</td>
              <td>{tarefa.prioridade}</td>
              <td>{tarefa.projetoId}</td>
              <td>
                <Link to={`/tarefa/atribuir/${tarefa.id}`}>Atribuir</Link>
              </td>
              <td>
                <Link to={`/tarefa/alterar/${tarefa.id}`}>Alterar</Link>
              </td>
              <td>
                <button onClick={() => Delete(tarefa.id!)}>Deletar</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ListarTarefas;
