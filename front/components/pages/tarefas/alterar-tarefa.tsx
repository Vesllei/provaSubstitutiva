import React, { useEffect, useState } from 'react';
import { Tarefa } from '../../../models/Tarefa';
import { useParams } from 'react-router-dom';

function AlterarTarefa() {
  const { id } = useParams<{ id: string }>();
  const [titulo, setTitulo] = useState('');
  const [descricao, setDescricao] = useState('');
  const [prazo, setPrazo] = useState('');
  const [prioridade, setPrioridade] = useState('');
  const [projetoId, setProjetoId] = useState('');
  const [atribuicoes, setAtribuicoes] = useState('');

  useEffect(() => {
    if (id) {
      carregarTarefa(id);
    }
  }, [id]);

  async function carregarTarefa(id: string) {
    try {
      const response = await fetch(`http://localhost:5028/api/tarefa/buscar/${id}`);
      if (!response.ok) {
        throw new Error('Erro ao buscar tarefa');
      }
      const data = await response.json();
      setTitulo(data.titulo);
      setDescricao(data.descricao);
      setPrazo(data.prazo);
      setPrioridade(data.prioridade);
      setProjetoId(data.projetoId);
      setAtribuicoes(data.atribuicoes);
    } catch (error) {
      console.error('Erro ao carregar tarefa:', error);
    }
  }

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();
    const tarefa: Tarefa = { titulo, descricao, prazo, prioridade, projetoId, atribuicoes};

    try {
      const response = await fetch(`http://localhost:5251/api/tarefa/alterar/${id}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(tarefa),
      });

      if (!response.ok) {
        throw new Error('Erro ao alterar tarefa');
      }

      const data = await response.json();
      console.log('Tarefa alterada:', data);
    } catch (error) {
      console.error('Erro ao alterar tarefa:', error);
    }
  }

  return (
    <div>
      
      <h1>Alterar Tarefa</h1>
      <form onSubmit={handleSubmit}>
        <label>Título:</label>
        <input type="text" value={titulo} onChange={(e) => setTitulo(e.target.value)} required />
        <br />
        <label>Descrição:</label>
        <input type="text" value={descricao} onChange={(e) => setDescricao(e.target.value)} required />
        <br />
        <label>Prazo:</label>
        <input type="datetime-local" value={prazo} onChange={(e) => setPrazo(e.target.value)} required />
        <br />
        <label>Prioridade:</label>
        <input type="text" value={prioridade} onChange={(e) => setPrioridade(e.target.value)} required />
        <br />
        <label>ID do Projeto:</label>
        <input type="text" value={projetoId} onChange={(e) => setProjetoId(e.target.value)} required />
        <br />
        <label>Atribuições:</label>
        <input type="text" value={atribuicoes} onChange={(e) => setAtribuicoes(e.target.value)} required />
        <br />
        <button type="submit">Alterar</button>
      </form>
    </div>
  );
}

export default AlterarTarefa;
