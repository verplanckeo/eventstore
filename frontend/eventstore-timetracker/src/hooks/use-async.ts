import { useEffect, useRef, useState, type DependencyList } from "react";

type AsyncFn<T> = () => Promise<T>;

interface AsyncState<T, E = Error> {
  loading: boolean;
  error: E | null;
  data: T | null;
}

// This is a hook as an alternative to React Query or SWR for handling async operations in React components.
// Post of Daniel Scott on Medium.com
// Did a few minor changes to make it more type-safe.

/**
 * useAsync
 * Runs an async function whenever `deps` change and exposes { loading, error, data }.
 * @param fn - The async function to run.
 * @param deps - Dependency array to control when the async function is called.
 * @return An object containing the loading state, error, and data.
 * @template T - The type of data returned by the async function.
 * @template E - The type of error that can be thrown by the async function (default is Error).
 * @example const { data, loading, error } = useAsync(() => fetchDataFromApi(), []);
 * @example const { data, loading, error, run } = useAsync(); useEffect(() => { run(() => fetchDataFromApi()); }, []);
 */
export function useAsync<T, E = Error>(
    fn: AsyncFn<T>,
    deps: DependencyList = []
): AsyncState<T, E> {
    const [state, setState] = useState<AsyncState<T, E>>({
        loading: true,
        error: null,
        data: null,
    });

    const mounted = useRef(true);

    useEffect(() => {
        mounted.current = true;
        setState({ loading: true, error: null, data: null });

        (async () => {
            try {
                const data = await fn();
                if (mounted.current) {
                    setState({ loading: false, error: null, data });
                }
            } catch (err: unknown) {
                if (mounted.current) {
                    const error =
                        (err instanceof Error ? err : new Error(String(err))) as E;
                    setState({ loading: false, error, data: null });
                }
            }
        })();

        return () => {
            mounted.current = false;
        };
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, deps);

    return state;
}